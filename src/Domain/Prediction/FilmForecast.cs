namespace Domain.Prediction;

public sealed class FilmForecast
{
    public Guid FilmId { get; set; }

    public TimeSpan EstimatedDuration { get; set; }

    public decimal EstimatedCost { get; set; }

    public double RiskScore { get; set; } // 0–1

    public string Summary { get; set; } = "";
}
