namespace Domain.Vision;

public sealed class ConsistencyScore
{
    public double CharacterScore { get; set; }

    public double EnvironmentScore { get; set; }

    public double PropScore { get; set; }

    public double OverallScore { get; set; }
}
