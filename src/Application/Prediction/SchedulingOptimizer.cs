using Domain.Jobs;

namespace Application.Prediction;

public sealed class SchedulingOptimizer
{
    public double Score(RenderJob job)
    {
        var complexity = job.Prompt.Length;
        var stabilityBias = 1.0;

        // placeholder learning logic
        if (complexity < 50) stabilityBias += 0.2;

        return complexity * stabilityBias;
    }
}
