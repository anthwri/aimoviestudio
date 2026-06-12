namespace Application.Analytics;

public sealed class FilmBurnRateTracker
{
    private readonly Dictionary<Guid, DateTime> _start = new();

    public void Start(Guid filmId)
    {
        _start[filmId] = DateTime.UtcNow;
    }

    public TimeSpan GetElapsed(Guid filmId)
    {
        return DateTime.UtcNow - _start[filmId];
    }

    public decimal GetBurnRate(decimal costSoFar, Guid filmId)
    {
        var hours = GetElapsed(filmId).TotalHours;

        if (hours == 0) return 0;

        return costSoFar / (decimal)hours;
    }
}
