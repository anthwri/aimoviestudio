using Domain.Jobs;
using Domain.Production;

namespace Application.Prediction;

public sealed class WorkloadEstimator
{
    public double EstimateRenderSeconds(RenderJob job)
    {
        // crude heuristic model (later replaced with ML)
        var baseTime = 2.5;
        var complexity = job.Prompt.Length / 120.0;

        return baseTime + complexity;
    }

    public decimal EstimateCost(RenderJob job)
    {
        var seconds = EstimateRenderSeconds(job);
        return (decimal)seconds * 0.0003m; // GPU cost/sec
    }
}
