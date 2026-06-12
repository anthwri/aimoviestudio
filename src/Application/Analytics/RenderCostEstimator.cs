using Domain.Jobs;

namespace Application.Analytics;

public sealed class RenderCostEstimator
{
    public decimal Estimate(RenderJob job)
    {
        var baseCost = 0.01m;

        var complexity = job.Prompt.Length / 100m;

        return baseCost + complexity * 0.05m;
    }
}
