using Application.Interfaces.Persistence;
using Domain.Jobs;

namespace Application.Inspection;

public sealed class ShotInspectorService
{
    private readonly IFilmEventRepository _events;

    public ShotInspectorService(IFilmEventRepository events)
    {
        _events = events;
    }

    public async Task<List<FilmEvent>> InspectFilm(Guid filmId)
    {
        return await _events.GetByFilmAsync(filmId, CancellationToken.None);
    }
}
