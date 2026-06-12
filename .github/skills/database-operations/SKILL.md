---
name: database-operations
description: "Implement Entity Framework Core patterns, repositories, and database operations for AiMovieStudio. Use when: setting up DbContext, creating repositories, managing relationships, implementing queries, or working with migrations."
---

# Database Operations

Master Entity Framework Core patterns, repository implementations, and database operations for AiMovieStudio.

## When to Use This Skill

- Setting up **DbContext** and **DbSets** for new entities
- Creating **Repository pattern** implementations
- Implementing **relationship queries** and includes
- Managing **CancellationToken** in async data access
- Configuring **connection strings** and **database setup**
- Working with **Code-First migrations**

---

## DbContext Setup

### MovieDbContext Pattern
```csharp
namespace Infrastructure.Database;

public sealed class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }
    
    // Core entities
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Film> Films => Set<Film>();
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Scene> Scenes => Set<Scene>();
    public DbSet<Shot> Shots => Set<Shot>();
    public DbSet<ContinuityRecord> ContinuityRecords => Set<ContinuityRecord>();
    public DbSet<StoryboardFrame> StoryboardFrames => Set<StoryboardFrame>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure entity relationships and constraints
        ConfigureMovieEntity(modelBuilder);
        ConfigureCharacterEntity(modelBuilder);
        ConfigureSceneEntity(modelBuilder);
    }
    
    private void ConfigureMovieEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasKey(m => m.Id);
        
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Characters)
            .WithOne()
            .HasForeignKey("MovieId")
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Scenes)
            .WithOne()
            .HasForeignKey("MovieId")
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void ConfigureCharacterEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<Character>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        modelBuilder.Entity<Character>()
            .Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(2000);
    }
    
    private void ConfigureSceneEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Scene>()
            .HasKey(s => s.Id);
        
        modelBuilder.Entity<Scene>()
            .HasMany(s => s.Shots)
            .WithOne()
            .HasForeignKey("SceneId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

### Connection String Configuration
```csharp
// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AiMovieStudio;Username=postgres;Password=postgres;",
    "Neo4jConnection": "bolt://localhost:7687",
    "QdrantConnection": "http://localhost:6333"
  }
}

// Infrastructure/DependencyInjection.cs
public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
{
    var connectionString = config.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("DefaultConnection not found");
    
    services.AddDbContext<MovieDbContext>(options =>
        options.UseNpgsql(connectionString));
    
    return services;
}
```

---

## Repository Pattern

### Generic Repository Base
```csharp
namespace Infrastructure.Repositories;

public abstract class RepositoryBase<T> where T : class
{
    protected readonly MovieDbContext Context;
    
    protected RepositoryBase(MovieDbContext context)
    {
        Context = context;
    }
    
    protected IQueryable<T> GetQuery() => Context.Set<T>();
    
    public async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync(ct);
        return entity;
    }
    
    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync(ct);
    }
    
    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync(ct);
    }
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await Context.SaveChangesAsync(ct);
}
```

### Specific Repository Implementation
```csharp
namespace Infrastructure.Repositories;

public interface IMovieRepository
{
    Task<Movie> AddAsync(Movie movie, CancellationToken ct);
    Task<Movie?> GetAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Movie>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<Movie>> GetByGenreAsync(string genre, CancellationToken ct);
    Task UpdateAsync(Movie movie, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}

public sealed class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(MovieDbContext context) : base(context)
    {
    }
    
    public async Task<Movie> AddAsync(Movie movie, CancellationToken ct)
    {
        Context.Movies.Add(movie);
        await Context.SaveChangesAsync(ct);
        return movie;
    }
    
    public async Task<Movie?> GetAsync(Guid id, CancellationToken ct)
        => await Context.Movies
            .Include(m => m.Characters)
            .Include(m => m.Scenes)
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    
    public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken ct)
        => await Context.Movies
            .AsNoTracking()
            .ToListAsync(ct);
    
    public async Task<IEnumerable<Movie>> GetByGenreAsync(
        string genre,
        CancellationToken ct)
        => await Context.Movies
            .Where(m => m.Genre == genre)
            .AsNoTracking()
            .ToListAsync(ct);
    
    public async Task UpdateAsync(Movie movie, CancellationToken ct)
    {
        Context.Movies.Update(movie);
        await Context.SaveChangesAsync(ct);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var movie = await GetAsync(id, ct);
        if (movie != null)
        {
            Context.Movies.Remove(movie);
            await Context.SaveChangesAsync(ct);
        }
    }
}
```

---

## Relationship Management

### One-to-Many Relationship
```csharp
// Domain entity
public sealed class Movie
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Scene> Scenes { get; set; } = [];
}

public sealed class Scene
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;
    
    // Foreign key
    public Guid MovieId { get; set; }
}

// DbContext configuration
modelBuilder.Entity<Movie>()
    .HasMany(m => m.Scenes)
    .WithOne()
    .HasForeignKey(s => s.MovieId)
    .OnDelete(DeleteBehavior.Cascade);
```

### Many-to-Many Relationship
```csharp
// Domain entities
public sealed class Character
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Scene> Appearances { get; set; } = [];
}

public sealed class Scene
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ICollection<Character> Characters { get; set; } = [];
}

// DbContext configuration
modelBuilder.Entity<Character>()
    .HasMany(c => c.Appearances)
    .WithMany(s => s.Characters)
    .UsingEntity("CharacterSceneAppearance");
```

---

## Query Patterns

### Eager Loading with Include
```csharp
// Load related entities to avoid N+1 queries
var movie = await Context.Movies
    .Include(m => m.Characters)
    .Include(m => m.Scenes)
        .ThenInclude(s => s.Shots)
    .FirstOrDefaultAsync(m => m.Id == id, ct);
```

### Filtering & Pagination
```csharp
public async Task<(IEnumerable<Movie> Items, int Total)> GetPagedAsync(
    int page,
    int pageSize,
    CancellationToken ct)
{
    var query = Context.Movies.AsQueryable();
    
    var total = await query.CountAsync(ct);
    var items = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .AsNoTracking()
        .ToListAsync(ct);
    
    return (items, total);
}
```

### Filtering by Criteria
```csharp
public async Task<IEnumerable<Movie>> SearchAsync(
    string? title,
    string? genre,
    CancellationToken ct)
{
    var query = Context.Movies.AsQueryable();
    
    if (!string.IsNullOrWhiteSpace(title))
        query = query.Where(m => m.Title.Contains(title));
    
    if (!string.IsNullOrWhiteSpace(genre))
        query = query.Where(m => m.Genre == genre);
    
    return await query.AsNoTracking().ToListAsync(ct);
}
```

### Aggregation Queries
```csharp
public async Task<int> GetMovieCountAsync(CancellationToken ct)
    => await Context.Movies.CountAsync(ct);

public async Task<IEnumerable<(string Genre, int Count)>> GetGenreStatisticsAsync(CancellationToken ct)
    => await Context.Movies
        .GroupBy(m => m.Genre)
        .Select(g => new ValueTuple<string, int>(g.Key, g.Count()))
        .AsNoTracking()
        .ToListAsync(ct);
```

---

## EF Core Best Practices

### AsNoTracking for Read-Only Queries
```csharp
// Better performance for queries that don't modify data
var movies = await Context.Movies
    .AsNoTracking()
    .ToListAsync(ct);
```

### Use CancellationToken Consistently
```csharp
// ✅ Good
public async Task<Movie?> GetAsync(Guid id, CancellationToken ct)
    => await Context.Movies.FirstOrDefaultAsync(m => m.Id == id, ct);

// ❌ Bad (no CancellationToken)
public async Task<Movie?> GetAsync(Guid id)
    => await Context.Movies.FirstOrDefaultAsync(m => m.Id == id);
```

### Avoid Lazy Loading
```csharp
// ❌ Bad - Lazy loading causes N+1 queries
var movie = await Context.Movies.FirstOrDefaultAsync(m => m.Id == id);
var characters = movie.Characters; // Additional query

// ✅ Good - Eager load with Include
var movie = await Context.Movies
    .Include(m => m.Characters)
    .FirstOrDefaultAsync(m => m.Id == id);
```

### Batch Operations
```csharp
// ✅ Good - Single SaveChanges
var movies = new[] { movie1, movie2, movie3 };
Context.Movies.AddRange(movies);
await Context.SaveChangesAsync(ct);

// ❌ Bad - Multiple SaveChanges
Context.Movies.Add(movie1);
await Context.SaveChangesAsync(ct);
Context.Movies.Add(movie2);
await Context.SaveChangesAsync(ct);
```

---

## Migrations & Database Setup

### Create Initial Migration
```powershell
# From project root or Infrastructure project
dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/Api

# Apply migration
dotnet ef database update --project src/Infrastructure --startup-project src/Api

# Generate migration without applying
dotnet ef migrations add AddCharacterTable --project src/Infrastructure
```

### Migration Best Practices
```csharp
// Migrations in database/migrations/
namespace Infrastructure.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Logline = table.Column<string>(maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: DateTime.UtcNow)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Movies");
        }
    }
}
```

---

## DI Registration Pattern

```csharp
// Infrastructure/DependencyInjection.cs
namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection not found");
        
        services.AddDbContext<MovieDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        // Add repositories
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<ISceneRepository, SceneRepository>();
        services.AddScoped<IShotRepository, ShotRepository>();
        
        // Add LLM client
        services.AddScoped<ILlmClient, OllamaClient>();
        
        return services;
    }
}

// Api/Program.cs
builder.Services.AddInfrastructure(builder.Configuration);
```

---

## Common Patterns Checklist

- [ ] DbContext uses **sealed class** pattern
- [ ] DbSet properties use **Set<T>()** expression
- [ ] **OnModelCreating** configures relationships
- [ ] Repository implements **IRepository<T>** interface
- [ ] All methods use **CancellationToken**
- [ ] Queries use **.Include()** to avoid N+1
- [ ] Read-only queries use **.AsNoTracking()**
- [ ] Foreign keys use **OnDelete(DeleteBehavior.Cascade)**
- [ ] Repositories registered in **DependencyInjection.cs**
- [ ] Connection string in **appsettings.json**

---

## Troubleshooting

| Problem | Solution |
|---------|----------|
| Connection failed | Check connection string; ensure PostgreSQL running |
| Migration fails | Check DbContext configuration; verify SQL syntax |
| N+1 query problem | Add `.Include()` to eager load relationships |
| Slow queries | Use `.AsNoTracking()` for read-only queries |
| Duplicate records | Use `.FirstOrDefaultAsync()` instead of `.ToListAsync()` |
| Navigation property null | Add `.Include()` before querying |
| SaveChanges fails | Check entity relationships; validate foreign keys |

---

## Quick Reference

```powershell
# Setup database
dotnet ef database update --project src/Infrastructure --startup-project src/Api

# Create migration
dotnet ef migrations add MigrationName --project src/Infrastructure

# Remove last migration
dotnet ef migrations remove --project src/Infrastructure

# View current database schema
# Use pgAdmin or psql to connect to PostgreSQL
```

