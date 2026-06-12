using Application.Interfaces.Persistence;

namespace Application.Analytics;

public sealed class FilmAnalyticsService
{
    private readonly IFilmEventRepository _events;

    public FilmAnalyticsService(IFilmEventRepository events)
    {
        _events = events;
    }

    public async Task<object> GetStats(Guid filmId)
    {
        var timeline = await _events.GetByFilmAsync(filmId, CancellationToken.None);

        var total = timeline.Count;
        var failed = timeline.Count(e => e.EventType == "failed");
        var completed = timeline.Count(e => e.EventType == "completed");

        return new
        {
            TotalEvents = total,
            Completed = completed,
            Failed = failed,
            SuccessRate = total == 0 ? 0 : (double)completed / total
        };
    }
}
