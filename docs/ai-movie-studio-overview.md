# AiMovieStudio Overview

## Purpose
AiMovieStudio is a full-stack AI-powered movie generation platform. It uses a local LLM backend to generate film concepts, story elements, and studio-level planning through AI agents.

## Architecture

### Layers
- **API Layer**: `src/Api/` hosts ASP.NET Core controllers and application entrypoint.
- **Application Layer**: `src/Application/` contains use cases, orchestration, studio workflows, and evaluation logic.
- **Domain Layer**: `src/Domain/` defines entities such as movies, characters, scenes, and studio state.
- **Infrastructure Layer**: `src/Infrastructure/` provides persistence, LLM client integration, and DI configuration.
- **Agents Layer**: `src/Agents/` contains specialized AI agents like `DirectorAgent` and `ShotGeneratorAgent`.

## Main Components

### Backend
- `src/Api/Program.cs` bootstraps the app and wires infrastructure.
- `Infrastructure/DependencyInjection.cs` registers `MovieDbContext`, `ILlmClient` via `OllamaClient`, and repositories.
- `Infrastructure/Ollama/OllamaClient.cs` sends prompts to a local Ollama server and returns generated text.

### AI Agents
- `DirectorAgent` builds a structured prompt to generate a complete movie concept.
- `MultiAgentStudioOrchestrator` simulates a studio meeting with multiple roles: director, cinematographer, editor, and producer.
- `AutonomousStudioOrganism` represents an autonomous studio intelligence that reasons over studio state and triggers.
- Additional studio components exist for debate, franchise evolution, continuity, and learning.

### Frontend
- `studio-ui/` is the Vue 3 frontend.
- The UI includes a `StudioDashboard.vue` view that fetches studio state from `api/studio/state`.
- Frontend wiring appears minimal and may be incomplete in some API integration areas.

## Data Flow
1. User submits a movie idea to the backend.
2. `MoviesController` calls `DirectorAgent.GenerateAsync()`.
3. `DirectorAgent` crafts a JSON-only prompt for the LLM.
4. `OllamaClient` forwards the prompt to the local Ollama API.
5. The model response is deserialized into a `MovieResponse` object.
6. The API returns the structured movie concept to the caller.

## Notes
- The system is designed around clean architecture and dependency injection.
- It aims to support a broader studio concept beyond movie generation, including franchise planning and studio memory.
- The current concrete path is mainly movie generation and studio state retrieval; some advanced studio features appear exploratory or partially built.

## Running the Studio
- Backend: run `dotnet restore`, `dotnet build`, then `dotnet run --project src/Api/Api.csproj`.
- Frontend: `cd studio-ui`, `npm install`, `npm run dev`.
- Ollama: run `ollama serve` and ensure the model is available.
