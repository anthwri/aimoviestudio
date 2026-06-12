---
name: create-csharp-feature
description: "Rapidly scaffold new C# features across Domain, Application, Infrastructure, and API layers following AiMovieStudio conventions. Use when: adding a new movie feature, entity type, or domain concept to the clean architecture."
---

# Create C# Feature

Scaffold complete, convention-compliant features across all layers of the clean architecture.

## When to Use This Skill

- Adding a new **Domain entity** (Movie feature, Character, Location, etc.)
- Creating corresponding **Application handlers** and use cases
- Scaffolding **Infrastructure repositories** and database access
- Building **API Controller** with standard endpoints
- Registering services in **Dependency Injection**

---

## Feature Architecture Template

```
Feature: {FeatureName} (e.g., "Movies", "Characters")

1. Domain Layer (src/Domain/{FeatureName}/)
   ├── {EntityName}.cs           # Root aggregate or entity
   ├── {RelatedEntity}.cs        # Related entities
   └── Value Objects (if needed)

2. Application Layer (src/Application/{FeatureName}/)
   ├── Create{EntityName}Handler.cs
   ├── Get{EntityName}Handler.cs
   ├── Update{EntityName}Handler.cs
   ├── Delete{EntityName}Handler.cs
   ├── Queries/
   │   └── Get{EntityName}Query.cs
   └── Commands/
       ├── Create{EntityName}Command.cs
       ├── Update{EntityName}Command.cs
       └── Delete{EntityName}Command.cs

3. Infrastructure Layer (src/Infrastructure/)
   ├── Repositories/{EntityName}Repository.cs
   ├── Database/MovieDbContext.cs  # Add DbSet<{EntityName}>

4. Contracts Layer (src/Contracts/)
   ├── Requests/Create{EntityName}Request.cs
   ├── Requests/Update{EntityName}Request.cs
   └── Responses/{EntityName}Response.cs

5. API Layer (src/Api/Controllers/)
   └── {EntityName}Controller.cs
       ├── POST /api/{entities}
       ├── GET /api/{entities}/{id}
       ├── PUT /api/{entities}/{id}
       └── DELETE /api/{entities}/{id}
```

---

## Step-by-Step Workflow

### 1. Define the Feature
Ask the user:
- Feature name (e.g., "Movies", "Characters")
- Primary entity name (e.g., "Movie", "Character")
- Related entities or sub-aggregates
- Key properties/fields
- Any special business logic

### 2. Create Domain Entity
```csharp
namespace Domain.Movies;

public sealed class Movie
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Logline { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public ICollection<Character> Characters { get; set; } = [];
    public ICollection<Scene> Scenes { get; set; } = [];
}
```

**Key Patterns:**
- Use **sealed classes**
- Auto-generate Guid with `Guid.NewGuid()`
- Default empty strings/empty collections
- Use file-scoped namespaces
- Property initialization in property declaration

### 3. Create Application Layer (Handler + Commands)
```csharp
// Application/Movies/Commands/CreateMovieCommand.cs
namespace Application.Movies.Commands;

public sealed record CreateMovieCommand(string Title, string Logline) : IRequest<MovieResponse>;

// Application/Movies/CreateMovieHandler.cs
public sealed class CreateMovieHandler : IRequestHandler<CreateMovieCommand, MovieResponse>
{
    private readonly IMovieRepository _repository;
    
    public CreateMovieHandler(IMovieRepository repository) => _repository = repository;
    
    public async Task<MovieResponse> Handle(
        CreateMovieCommand request,
        CancellationToken ct)
    {
        var movie = new Movie { Title = request.Title, Logline = request.Logline };
        await _repository.AddAsync(movie, ct);
        return new MovieResponse(movie.Id, movie.Title, movie.Logline);
    }
}
```

### 4. Create Repository Interface & Implementation
```csharp
// Application/Movies/IMovieRepository.cs
namespace Application.Movies;

public interface IMovieRepository
{
    Task<Movie> AddAsync(Movie movie, CancellationToken ct);
    Task<Movie?> GetAsync(Guid id, CancellationToken ct);
    Task UpdateAsync(Movie movie, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}

// Infrastructure/Repositories/MovieRepository.cs
namespace Infrastructure.Repositories;

public sealed class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _context;
    
    public MovieRepository(MovieDbContext context) => _context = context;
    
    public async Task<Movie> AddAsync(Movie movie, CancellationToken ct)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync(ct);
        return movie;
    }
    
    public async Task<Movie?> GetAsync(Guid id, CancellationToken ct)
        => await _context.Movies.FirstOrDefaultAsync(m => m.Id == id, ct);
}
```

### 5. Update DbContext
```csharp
// Infrastructure/Database/MovieDbContext.cs
public sealed class MovieDbContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Character> Characters => Set<Character>();
    // ... add DbSet for your entity
}
```

### 6. Create DTOs
```csharp
// Contracts/Requests/CreateMovieRequest.cs
public sealed record CreateMovieRequest(string Title, string Logline);

// Contracts/Responses/MovieResponse.cs
public sealed record MovieResponse(Guid Id, string Title, string Logline);
```

### 7. Create Controller
```csharp
// Api/Controllers/MoviesController.cs
[ApiController]
[Route("api/[controller]")]
public sealed class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public MoviesController(IMediator mediator) => _mediator = mediator;
    
    [HttpPost]
    public async Task<ActionResult<MovieResponse>> Create(
        CreateMovieRequest request,
        CancellationToken ct)
    {
        var command = new CreateMovieCommand(request.Title, request.Logline);
        var result = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovieResponse>> Get(
        Guid id,
        CancellationToken ct)
    {
        var query = new GetMovieQuery(id);
        var result = await _mediator.Send(query, ct);
        return result == null ? NotFound() : Ok(result);
    }
}
```

### 8. Register Services in DI
```csharp
// Infrastructure/DependencyInjection.cs
public static IServiceCollection AddInfrastructure(this IServiceCollection services)
{
    services.AddScoped<IMovieRepository, MovieRepository>();
    // ... register other repositories
    return services;
}

// Api/Program.cs
builder.Services
    .AddInfrastructure()
    .AddApplication();
```

---

## Naming Conventions Checklist

- [ ] Entity: `{Name}` (sealed) - `Movie`, `Character`
- [ ] Interface: `I{Name}` - `IMovieRepository`
- [ ] Command: `Create/Update/Delete{Name}Command` - `CreateMovieCommand`
- [ ] Query: `Get{Name}Query` - `GetMovieQuery`
- [ ] Handler: `{Name}Handler` - `CreateMovieHandler`
- [ ] Request DTO: `{Name}Request` - `CreateMovieRequest`
- [ ] Response DTO: `{Name}Response` - `MovieResponse`
- [ ] Repository: `{Name}Repository` - `MovieRepository`
- [ ] Controller: `{Name}Controller` - `MoviesController`
- [ ] Route: `api/{plural}` - `api/movies`

---

## File Organization Checklist

- [ ] Domain entity in `src/Domain/{Feature}/{EntityName}.cs`
- [ ] Commands in `src/Application/{Feature}/Commands/`
- [ ] Queries in `src/Application/{Feature}/Queries/`
- [ ] Handlers inherit from `IRequestHandler<,>` (MediatR)
- [ ] Repository interface in `src/Application/{Feature}/`
- [ ] Repository implementation in `src/Infrastructure/Repositories/`
- [ ] DbSet added to `MovieDbContext`
- [ ] Request/Response DTOs in `src/Contracts/Requests/` and `src/Contracts/Responses/`
- [ ] Controller in `src/Api/Controllers/`
- [ ] Services registered in `Infrastructure/DependencyInjection.cs`

---

## Common Patterns

### Sealed Classes
```csharp
public sealed class MovieRepository : IMovieRepository { }
public sealed class CreateMovieHandler : IRequestHandler<CreateMovieCommand, MovieResponse> { }
```

### File-Scoped Namespaces
```csharp
namespace Domain.Movies;

public sealed class Movie { }
```

### Constructor Dependency Injection
```csharp
public sealed class CreateMovieHandler
{
    private readonly IMovieRepository _repository;
    
    public CreateMovieHandler(IMovieRepository repository) => _repository = repository;
}
```

### Async/Await with CancellationToken
```csharp
public async Task<MovieResponse> Handle(
    CreateMovieCommand request,
    CancellationToken ct)
{
    await _repository.AddAsync(movie, ct);
}
```

### JSON Response Handling
```csharp
var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
var result = JsonSerializer.Deserialize<MovieResponse>(json, options);
```

---

## Testing Checklist

- [ ] Build project: `dotnet build`
- [ ] Test endpoint in Swagger: `http://localhost:5000/swagger`
- [ ] Verify database record persists in PostgreSQL
- [ ] Check JSON serialization (use debugger)
- [ ] Confirm CORS allows frontend calls from `localhost:5173`

---

## Quick Reference: Commands to Run

```powershell
# Build and run
dotnet build
dotnet run --project src/Api/Api.csproj

# Verify in Swagger
# Navigate to http://localhost:5000/swagger

# Test with Powershell
$body = @{ title = "My Movie"; logline = "A test story" } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5000/api/movies" -Method Post -Body $body -ContentType "application/json"
```

