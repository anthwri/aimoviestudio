namespace Domain.Executive;

public sealed class AudienceSimulationResult
{
    public Guid FilmId { get; set; }

    public double PredictedEngagement { get; set; }   // 0–1

    public double ConfusionRisk { get; set; }         // 0–1

    public double ViralPotential { get; set; }        // 0–1

    public string Summary { get; set; } = "";
}
