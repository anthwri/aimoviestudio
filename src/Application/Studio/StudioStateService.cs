using Domain.Studio;

namespace Application.Studio;

public sealed class StudioStateService
{
    public StudioState Build(
        int pending,
        int running,
        int failed,
        int completed,
        IEnumerable<double> scores)
    {
        return new StudioState
        {
            PendingJobs = pending,
            RunningJobs = running,
            FailedJobs = failed,
            CompletedJobs = completed,
            AverageConsistencyScore =
                scores.Any() ? scores.Average() : 0,
            EstimatedCost =
                completed * 0.01m
        };
    }
}
