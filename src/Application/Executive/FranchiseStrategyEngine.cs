using Domain.Executive;

namespace Application.Executive;

public sealed class FranchiseStrategyEngine
{
    public FranchiseValue Evaluate(
        Guid filmId,
        double cost,
        double engagement,
        double viral,
        double confusion)
    {
        var narrativeValue = (engagement * 0.5) + (viral * 0.3) - (confusion * 0.2);

        var marketPotential = (engagement + viral) / 2;

        var priority =
            (marketPotential * 0.6) +
            (narrativeValue * 0.4) -
            (cost * 0.01);

        return new FranchiseValue
        {
            FilmId = filmId,
            EstimatedCost = (decimal)cost,
            NarrativeValue = narrativeValue,
            MarketPotential = marketPotential,
            StrategicPriority = priority
        };
    }
}
