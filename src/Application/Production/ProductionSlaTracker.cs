namespace Application.Production;

public sealed class ProductionSlaTracker
{
    private readonly Dictionary<Guid, DateTime> _startTimes = new();

    public void Start(Guid filmId)
    {
        _startTimes[filmId] = DateTime.UtcNow;
    }

    public TimeSpan? GetElapsed(Guid filmId)
    {
        if (!_startTimes.ContainsKey(filmId))
            return null;

        return DateTime.UtcNow - _startTimes[filmId];
    }

    public bool IsOverdue(Guid filmId, TimeSpan slaLimit)
    {
        var elapsed = GetElapsed(filmId);
        return elapsed.HasValue && elapsed > slaLimit;
    }
}
