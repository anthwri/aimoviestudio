namespace Domain.Learning;

public sealed class FilmOutcome
{
    public Guid FilmId { get; set; }

    public double AudienceScore { get; set; }

    public double RevenueScore { get; set; }

    public double CriticalScore { get; set; }

    public double CompletionQuality { get; set; }

    public DateTime ReleasedAt { get; set; } = DateTime.UtcNow;
}
