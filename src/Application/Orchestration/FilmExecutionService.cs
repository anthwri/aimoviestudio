using Application.Interfaces.Persistence;
using Contracts.Director;
using Domain.Movies;

namespace Application.Orchestration;

public sealed class FilmExecutionService
{
    private readonly IFilmRepository _repo;
    private readonly FilmOrchestrator _orchestrator;

    public FilmExecutionService(
        IFilmRepository repo,
        FilmOrchestrator orchestrator)
    {
        _repo = repo;
        _orchestrator = orchestrator;
    }

    public async Task<Film> ExecuteAsync(
        CreateFilmRequest request,
        CancellationToken ct)
    {
        var film = new Film
        {
            Title = request.Idea,
            Idea = request.Idea
        };

        await _repo.AddAsync(film, ct);

        var result = await _orchestrator.CreateFilmAsync(request, ct);

        // future: map result → DB updates

        return film;
    }
}
