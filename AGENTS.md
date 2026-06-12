# AI Agent Guidelines for AiMovieStudio

**AiMovieStudio** is a full-stack AI-powered movie generation platform that creates complete movie concepts (narratives, characters, locations, scenes, shots, storyboards) using local LLM orchestration.

## Project Type
- **Full-Stack Application**: C# ASP.NET Core backend + Vue 3 TypeScript frontend
- **Purpose**: AI-driven movie generation from prompts
- **Key Innovation**: Agents orchestrating multiple LLM calls to generate coherent, multi-faceted movie content

---

## Architecture Overview

### Clean Architecture Layers (Top-to-Bottom)

```
API Layer (Controllers, Hubs)
    ↓
Application Layer (Use cases, business logic, handlers)
    ↓
Domain Layer (Pure entities, business rules)
    ↓
Infrastructure Layer (Database, external services, LLM)
```

**Project Structure:**
- `src/Api/` - ASP.NET Core Web API, Controllers, SignalR Hubs, Swagger
- `src/Application/` - Use cases, MediatR handlers, business logic
- `src/Domain/` - Entities (Movie, Character, Scene, Shot, Film, etc.), value objects
- `src/Infrastructure/` - EF Core DbContext, repositories, LLM client, external services
- `src/Agents/` - AI agents (DirectorAgent, ShotGeneratorAgent, etc.)
- `src/Contracts/` - DTOs, Request/Response objects

**Frontend:**
- `studio-ui/` - Modern Vue 3 + TypeScript + Vite frontend (primary UI)
- `frontend/` - Legacy Vue frontend

---

## Quick Start

### Backend Setup
```powershell
# From project root
dotnet restore
dotnet build
dotnet run --project src/Api/Api.csproj

# API available at http://localhost:5000
# Swagger docs at http://localhost:5000/swagger
```

### Frontend Setup
```bash
cd studio-ui
npm install
npm run dev  # Dev server at http://localhost:5173
```

### Infrastructure (Database & Services)
```bash
cd docker  # or infrastructure/
docker-compose up -d

# Starts: PostgreSQL (5432), Neo4j (7687), Qdrant (6333)
# Credentials: postgres/postgres, neo4j/password123
```

### Ollama (Local LLM)
```bash
# Ensure Ollama service running
ollama serve

# In another terminal, pull required model
ollama pull qwen3:32b  # ~20GB
```

---

## Tech Stack

### Backend
- **Language & Framework**: C# 13, .NET 9.0, ASP.NET Core
- **Database**: PostgreSQL 17 (relational), Neo4j 5 (knowledge graph), Qdrant (vector DB)
- **ORM**: Entity Framework Core 10 (Code-First)
- **Patterns**: MediatR (CQRS), FluentValidation, Dependency Injection
- **LLM Integration**: Ollama client (local LLM access), Qwen 3 32B model
- **Real-time**: SignalR for WebSocket communication
- **API**: Minimal APIs, Swagger/OpenAPI

### Frontend
- **Framework**: Vue 3
- **Language**: TypeScript
- **Build Tool**: Vite 8
- **HTTP**: Axios
- **Real-time**: SignalR client

---

## Code Conventions & Patterns

### C# Standards
- **Sealed classes** for concrete implementations (prevents accidental inheritance)
- **File-scoped namespaces**: `namespace Domain.Movies;`
- **Constructor dependency injection** standard
- **Nullable reference types** enabled project-wide
- **Implicit using directives** enabled in `.csproj`
- **Property-based data models** with sensible defaults

### Naming Conventions
| Element | Pattern | Example |
|---------|---------|---------|
| Interfaces | `I{Name}` | `ILlmClient`, `IMovieRepository` |
| Concrete Classes | `{Name}` (sealed) | `DirectorAgent`, `MovieRepository` |
| Request DTOs | `{Entity}{Action}Request` | `CreateMovieRequest`, `GenerateShotsRequest` |
| Response DTOs | `{Entity}Response` | `MovieResponse`, `CharacterResponse` |
| Controllers | `{Entity}Controller` | `MoviesController`, `CharactersController` |
| DbContext | `{Feature}DbContext` | `MovieDbContext` |
| Repositories | `{Entity}Repository` | `MovieRepository`, `FilmRepository` |

### Folder Organization (Layer-Based + Feature-Based)
Each layer mirrors domain features. Example for Movies feature:
```
Domain/Movies/
├── Movie.cs          # Root aggregate
├── Film.cs
└── [related entities]

Application/Movies/
├── CreateMovieHandler.cs
├── Queries/
└── Commands/

Infrastructure/
├── Repositories/MovieRepository.cs
├── LLM/OllamaClient.cs
└── Database/MovieDbContext.cs

Api/Controllers/
└── MoviesController.cs
```

### API Route Conventions
- **Base path**: `api/{resource}`
- **Standard verbs**: GET (read), POST (create/generate), PUT (update), DELETE (remove)
- **Async methods**: All endpoints use `async Task<ActionResult<T>>` with `CancellationToken`
- **Example**: `POST /api/movies/generate` with `GenerateMovieRequest` payload

### AI Agent Pattern
```csharp
public sealed class DirectorAgent
{
    private readonly ILlmClient _llm;
    
    public DirectorAgent(ILlmClient llm) => _llm = llm;
    
    public async Task<MovieResponse> GenerateAsync(
        string movieIdea,
        CancellationToken ct = default)
    {
        var prompt = $@"You are an elite film director...
        Generate movie from: {movieIdea}
        Return ONLY valid JSON...";
        
        var result = await _llm.PromptAsync(prompt, ct);
        return JsonSerializer.Deserialize<MovieResponse>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}
```

**Key Points:**
- Agents are dependency-injected services
- Depend on `ILlmClient` interface abstraction
- Use structured prompts; always expect JSON responses
- Handle `CancellationToken` for graceful shutdown
- Sealed classes prevent accidental inheritance

---

## Essential Files & Locations

| Purpose | Path |
|---------|------|
| **Solution file** | `AiMovieStudio.sln` |
| **Backend entry point** | `src/Api/Program.cs` |
| **Database context** | `src/Infrastructure/Database/MovieDbContext.cs` |
| **LLM client** | `src/Infrastructure/LLM/OllamaClient.cs` |
| **Domain entities** | `src/Domain/{Feature}/` (Movies, Characters, Scenes, etc.) |
| **API routes** | `src/Api/Controllers/` |
| **Frontend app** | `studio-ui/src/main.ts` |
| **Docker config** | `docker/docker-compose.yml` or `infrastructure/docker-compose.yml` |
| **Migrations** | `database/migrations/` (currently minimal) |
| **Docs** | `docs/{api,architecture,agents,database}/` (structure ready, content pending) |

---

## Database Schema Patterns

### Key Entities (Domain Layer)
- **Movie** - Root aggregate, contains title, logline, metadata
- **Film** - Sub-aggregate of Movie
- **Character** - Entities with personality, wardrobe, description
- **Location** - Environments with descriptions
- **Scene** - Acts containing sequences
- **Shot** - Cinematography frames (camera angle, description, timing)
- **ContinuityRecord** - Tracking consistency across scenes
- **StoryboardFrame** - Visual narrative frames

### Repository Pattern
- All data access through `IRepository<T>` abstraction
- Implementations in Infrastructure layer
- Use `CancellationToken` in all async methods
- Repositories registered in DI container via `DependencyInjection.cs`

---

## Common Gotchas & Solutions

| Issue | Root Cause | Solution |
|-------|-----------|----------|
| PostgreSQL connection fails | Docker not running | `docker-compose up -d` in docker/ folder |
| Ollama service offline | Missing service startup | Run `ollama serve` in separate terminal |
| Model not found | Qwen3 model not downloaded | `ollama pull qwen3:32b` (first-time only, ~20GB) |
| Cannot instantiate sealed class | Sealed classes prevent direct instantiation | Use DI container or factory methods |
| API CORS errors | Frontend not whitelisted | Check `Program.cs` CORS configuration |
| SignalR connection drops | Port mismatch or incorrect config | Verify port 5000 in frontend SignalR client config |
| Vector DB not found | Qdrant container not running | Start container via `docker-compose.yml` |
| Neo4j auth fails | Wrong credentials | Default: username=neo4j, password=password123 |
| EF Core queries slow | No eager loading | Use `.Include()` for related entities |
| JSON deserialization fails | Case sensitivity mismatch | Use `PropertyNameCaseInsensitive = true` in JsonSerializerOptions |

---

## Development Workflow

### Adding a New Feature
1. **Create Domain entity** in `src/Domain/{Feature}/`
   - Inherit from base entity if available
   - Use Guid for ID with `Guid.NewGuid()`
   - Follow sealed class pattern

2. **Add DbSet** to `MovieDbContext`
   ```csharp
   public DbSet<MyEntity> MyEntities => Set<MyEntity>();
   ```

3. **Create Repository** interface in `src/Application/` and implementation in `src/Infrastructure/`

4. **Add Controller** in `src/Api/Controllers/` following naming conventions

5. **Register services** in `src/Infrastructure/DependencyInjection.cs` DI container

6. **Test with Swagger** at `http://localhost:5000/swagger`

### Testing Agent Output
1. Use Swagger to test API endpoints
2. Monitor Ollama terminal for prompt output
3. Check JSON deserialization with Visual Studio debugger
4. Verify database records in PostgreSQL

### Frontend Integration
- Use Axios for HTTP calls: `api/resource` endpoints
- Use SignalR for real-time updates
- Subscribe to `ContainerRegistry.signal` events

---

## Performance Considerations

- **LLM Response Time**: ~30-120 seconds depending on model complexity
- **Vector Operations**: Qdrant handles embedding storage/retrieval
- **Graph Queries**: Neo4j for relationship traversal
- **Connection Pooling**: EF Core connection strings in `appsettings.Development.json`

---

## When You Get Stuck

1. **Check existing agents** in `src/Agents/` for pattern examples
2. **Review Domain entities** in `src/Domain/` for data structure
3. **Inspect Controllers** in `src/Api/Controllers/` for API endpoint patterns
4. **Search for similar feature** - this is a complex project with 25+ controllers
5. **Check error logs** - Ollama output, PostgreSQL logs, .NET trace
6. **Verify services running** - PostgreSQL, Ollama, Neo4j, Qdrant via Docker

---

## Documentation References

- **Architecture Deep Dive**: See `docs/architecture/` (structure ready)
- **Agent Development**: See `docs/agents/` (structure ready)
- **API Endpoints**: See `docs/api/` (structure ready)
- **Database Schema**: See `docs/database/` (structure ready)
- **Frontend Setup**: See `studio-ui/README.md` and `frontend/SETUP.txt`

*Note: Documentation folders are ready for contribution. See opportunity below.*

---

## Next Steps for Agents

### Documentation Opportunities
- Create `docs/architecture/LAYERS.md` - Detailed layer responsibilities
- Create `docs/agents/AGENT_PATTERNS.md` - Agent development guide with examples
- Create `docs/database/SCHEMA.md` - Complete entity relationship diagram

### Common Agent Tasks
- Generate new movie from user prompt via DirectorAgent
- Add character to existing movie
- Create scene with specific continuity constraints
- Generate storyboard frames for scene
- Query knowledge graph for consistency checks
- Validate genre/tone continuity across movie

### Extension Areas
- Implement caching layer for LLM responses (via Redis)
- Add Postgres full-text search for movie discovery
- Enhance Neo4j knowledge graph for complex relationships
- Implement rate limiting for Ollama service
- Add vector similarity search for character/location reuse

