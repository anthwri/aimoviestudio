using Application.Interfaces.Persistence;
using Domain.Events;

namespace Application.Events;

public sealed class FilmTimelineService
{
    private readonly IFilmEventRepository _repo;

    public FilmTimelineService(IFilmEventRepository repo)
    {
        _repo = repo;
    }

    public Task RecordAsync(
        Guid filmId,
        string type,
        string message,
        Guid? sceneId = null,
        Guid? shotId = null)
    {
        var evt = new FilmEvent
        {
            FilmId = filmId,
            SceneId = sceneId,
            ShotId = shotId,
            EventType = type,
            Message = message
        };

        return _repo.AddAsync(evt, CancellationToken.None);
    }

    public Task<List<FilmEvent>> GetTimeline(Guid filmId)
    {
        return _repo.GetByFilmAsync(filmId, CancellationToken.None);
    }
}
