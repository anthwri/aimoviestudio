---
name: agent-development
description: "Develop AI agents for movie generation using the Ollama LLM client. Use when: creating DirectorAgent, ShotGeneratorAgent, or other agents that orchestrate LLM calls for structured movie content generation."
---

# Agent Development

Develop sophisticated AI agents that orchestrate LLM calls to generate movie content (narratives, characters, scenes, shots, storyboards).

## When to Use This Skill

- Building a new **Agent** that calls the LLM (e.g., `DirectorAgent`, `CharacterGeneratorAgent`)
- Implementing **prompt engineering** for specific movie elements
- Creating **multi-step agent workflows** that orchestrate multiple LLM calls
- Implementing **response validation** and JSON deserialization
- Integrating agents with **Controllers** and **MediatR handlers**

---

## Agent Architecture

```
Agent (src/Agents/{Domain}/)
    ├── ILlmClient → OllamaClient (dependency)
    ├── Structured Prompt (system message + user input)
    ├── LLM Call via Ollama
    ├── JSON Response Parsing
    └── Return Strongly-Typed Response
```

### Agent Pattern
```csharp
public sealed class DirectorAgent
{
    private readonly ILlmClient _llm;
    
    public DirectorAgent(ILlmClient llm) => _llm = llm;
    
    public async Task<MovieResponse> GenerateAsync(
        string movieIdea,
        CancellationToken ct = default)
    {
        var prompt = BuildPrompt(movieIdea);
        var response = await _llm.PromptAsync(prompt, ct);
        return ParseResponse<MovieResponse>(response);
    }
    
    private string BuildPrompt(string movieIdea) => $@"
You are an elite film director and screenwriter.
Generate a complete movie concept for: {movieIdea}
Return ONLY valid JSON with no markdown, no explanation.
{{
  ""title"": ""string"",
  ""logline"": ""string"",
  ""genre"": ""string"",
  ""themes"": [""string""]
}}";
    
    private T ParseResponse<T>(string json) =>
        JsonSerializer.Deserialize<T>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        ?? throw new InvalidOperationException("Failed to deserialize LLM response");
}
```

---

## Step-by-Step Agent Implementation

### 1. Define Agent Responsibility
Ask the user:
- What **movie element** does this agent generate? (narrative, character, scene, shot, storyboard)
- What **inputs** are required? (movie premise, character details, scene context)
- What **output structure** (DTO) is needed?
- Should it be a **single-step** or **multi-step** workflow?

### 2. Create Agent Class in `src/Agents/`
```csharp
namespace Agents.Director;

public sealed class DirectorAgent
{
    private readonly ILlmClient _llm;
    
    public DirectorAgent(ILlmClient llm) => _llm = llm;
}
```

### 3. Implement Generation Method
```csharp
public async Task<MovieResponse> GenerateAsync(
    string movieIdea,
    CancellationToken ct = default)
{
    var prompt = BuildPrompt(movieIdea);
    var response = await _llm.PromptAsync(prompt, ct);
    return ParseResponse<MovieResponse>(response);
}
```

### 4. Craft Structured Prompt
```csharp
private string BuildPrompt(string movieIdea) => $@"
SYSTEM: You are an elite film director with 20+ years experience.

TASK: Generate a complete, original movie concept.

INPUT: {movieIdea}

OUTPUT REQUIREMENTS:
- Return ONLY valid JSON (no markdown, no explanations, no code blocks)
- Ensure all fields are non-empty strings
- Include creative, cinematic details
- Follow this exact schema:

{{
    ""title"": ""compelling movie title"",
    ""logline"": ""one-sentence plot summary"",
    ""genre"": ""genre classification"",
    ""themes"": [""theme1"", ""theme2""],
    ""toneAndStyle"": ""description of visual style"",
    ""targetAudience"": ""target demographic"",
    ""estimatedRuntime"": 120
}}";
```

**Prompt Best Practices:**
- ✅ **Be explicit** about JSON format ("ONLY JSON, no markdown")
- ✅ **Use role-play** ("You are an elite film director")
- ✅ **Provide schema** with example fields
- ✅ **Specify constraints** ("non-empty strings", "creative details")
- ✅ **Avoid ambiguity** about output format
- ❌ Avoid vague instructions like "Generate a movie"
- ❌ Don't ask for multiple formats in one call

### 5. Implement JSON Parsing
```csharp
private T ParseResponse<T>(string json)
{
    try
    {
        var options = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        
        return JsonSerializer.Deserialize<T>(json, options)
            ?? throw new InvalidOperationException("Deserialization returned null");
    }
    catch (JsonException ex)
    {
        throw new InvalidOperationException(
            $"Failed to parse LLM response: {json}", ex);
    }
}
```

### 6. Create Response DTO (in `src/Contracts/Responses/`)
```csharp
namespace Contracts.Responses;

public sealed record MovieResponse(
    string Title,
    string Logline,
    string Genre,
    string[] Themes,
    string ToneAndStyle,
    string TargetAudience,
    int EstimatedRuntime);
```

### 7. Integrate with Application Layer Handler
```csharp
namespace Application.Movies.Commands;

public sealed record GenerateMovieCommand(string MovieIdea) : IRequest<MovieResponse>;

public sealed class GenerateMovieHandler : IRequestHandler<GenerateMovieCommand, MovieResponse>
{
    private readonly DirectorAgent _agent;
    
    public GenerateMovieHandler(DirectorAgent agent) => _agent = agent;
    
    public async Task<MovieResponse> Handle(
        GenerateMovieCommand request,
        CancellationToken ct)
    {
        return await _agent.GenerateAsync(request.MovieIdea, ct);
    }
}
```

### 8. Expose via Controller
```csharp
namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public MoviesController(IMediator mediator) => _mediator = mediator;
    
    [HttpPost("generate")]
    public async Task<ActionResult<MovieResponse>> Generate(
        [FromBody] GenerateMovieRequest request,
        CancellationToken ct)
    {
        var command = new GenerateMovieCommand(request.MovieIdea);
        var result = await _mediator.Send(command, ct);
        return Ok(result);
    }
}
```

### 9. Register Agent in Dependency Injection
```csharp
// Infrastructure/DependencyInjection.cs
public static IServiceCollection AddAgents(this IServiceCollection services)
{
    services.AddScoped<DirectorAgent>();
    services.AddScoped<CharacterGeneratorAgent>();
    services.AddScoped<SceneGeneratorAgent>();
    return services;
}

// Api/Program.cs
builder.Services.AddAgents();
```

---

## Multi-Step Agent Workflows

For complex features requiring multiple LLM calls:

```csharp
public sealed class CompleteMovieOrchestrator
{
    private readonly DirectorAgent _directorAgent;
    private readonly CharacterGeneratorAgent _characterAgent;
    private readonly SceneGeneratorAgent _sceneAgent;
    
    public CompleteMovieOrchestrator(
        DirectorAgent directorAgent,
        CharacterGeneratorAgent characterAgent,
        SceneGeneratorAgent sceneAgent)
    {
        _directorAgent = directorAgent;
        _characterAgent = characterAgent;
        _sceneAgent = sceneAgent;
    }
    
    public async Task<CompleteMovieResponse> GenerateCompleteMovieAsync(
        string premise,
        CancellationToken ct = default)
    {
        // Step 1: Generate movie concept
        var movie = await _directorAgent.GenerateAsync(premise, ct);
        
        // Step 2: Generate characters for movie
        var characters = await _characterAgent.GenerateAsync(
            movie.Title, 
            movie.Logline, 
            ct);
        
        // Step 3: Generate scenes
        var scenes = await _sceneAgent.GenerateAsync(
            movie.Title,
            characters,
            ct);
        
        return new CompleteMovieResponse(movie, characters, scenes);
    }
}
```

---

## Prompt Engineering Tips

### Use System Roles
```
"You are an elite film director with 20+ years experience and Emmy nominations."
```

### Provide Context
```
"Given this movie premise: {premise}
And these established characters: {characterDescriptions}
Generate 5 pivotal scenes..."
```

### Structure Output Schema
```json
{
    "title": "string",
    "description": "string",
    "duration": 120,
    "keyMoments": ["string"]
}
```

### Constrain Creativity
```
- Tone: Dark thriller with comedic relief
- Visual Style: Cinematic widescreen, neon-lit cyberpunk
- Target Length: 110-130 minutes
```

### Request Specific Details
```
Include:
- Character motivations
- Plot twists
- Visual metaphors
- Dialogue examples
```

---

## Error Handling & Validation

```csharp
public async Task<MovieResponse> GenerateWithValidationAsync(
    string movieIdea,
    CancellationToken ct = default)
{
    if (string.IsNullOrWhiteSpace(movieIdea))
        throw new ArgumentException("Movie idea cannot be empty");
    
    try
    {
        var prompt = BuildPrompt(movieIdea);
        var response = await _llm.PromptAsync(prompt, ct);
        
        if (string.IsNullOrWhiteSpace(response))
            throw new InvalidOperationException("Empty response from LLM");
        
        return ParseResponse<MovieResponse>(response);
    }
    catch (JsonException ex)
    {
        throw new InvalidOperationException(
            "LLM response was not valid JSON. Retry with clearer prompt.", ex);
    }
    catch (HttpRequestException ex)
    {
        throw new InvalidOperationException(
            "Failed to connect to Ollama service. Is it running?", ex);
    }
}
```

---

## Testing Agents

### 1. Test Prompt in Ollama CLI
```bash
# Direct test before integrating
ollama run qwen3:32b "You are a film director. Generate a movie title for: A space heist story"
```

### 2. Test Agent in Swagger
```
POST /api/movies/generate
{
    "movieIdea": "A detective solving crimes in a cyberpunk city"
}
```

### 3. Monitor Ollama Output
```bash
# In separate terminal, watch Ollama logs
tail -f ~/.ollama/logs/ollama.log  # Linux/Mac
Get-Content $env:APPDATA\Ollama\logs.log -Tail 50 -Wait  # Windows
```

### 4. Verify Response Quality
- ✅ Valid JSON structure
- ✅ No null/empty fields
- ✅ Reasonable creativity
- ✅ Coherent with input

---

## Performance & Optimization

| Concern | Strategy |
|---------|----------|
| **Slow responses** | Response time 30-120s is normal for Qwen 32B; consider streaming |
| **Memory usage** | Model runs in-process; monitor with Task Manager |
| **Token limits** | Qwen 32B supports 32K context; use summaries for long inputs |
| **Reliability** | Retry logic for network timeouts; validate JSON |
| **Caching** | Store generated movies to avoid regenerating |

---

## Common Patterns Checklist

- [ ] Agent inherits **ILlmClient** dependency
- [ ] Sealed class pattern: `public sealed class {Agent}Agent`
- [ ] Prompt includes **role**, **task**, **constraints**, **schema**
- [ ] JSON parsing uses **PropertyNameCaseInsensitive**
- [ ] All methods use **CancellationToken**
- [ ] Error handling for **network** and **JSON** failures
- [ ] Agent registered in **DependencyInjection.cs**
- [ ] Exposed via **MediatR handler** → **Controller** endpoint
- [ ] Response DTO in **Contracts/Responses/**

---

## Quick Reference: Ollama Setup

```powershell
# Ensure Ollama running
ollama serve

# In another terminal, verify model
ollama list

# Pull model if needed
ollama pull qwen3:32b  # ~20GB

# Test directly
ollama run qwen3:32b "Hello, world"
```

