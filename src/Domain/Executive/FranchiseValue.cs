namespace Domain.Executive;

public sealed class FranchiseValue
{
    public Guid FilmId { get; set; }

    public decimal EstimatedCost { get; set; }

    public double NarrativeValue { get; set; }

    public double MarketPotential { get; set; }

    public double StrategicPriority { get; set; }
}
