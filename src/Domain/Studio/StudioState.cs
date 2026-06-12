namespace Domain.Studio;

public sealed class StudioState
{
    public int PendingJobs { get; set; }

    public int RunningJobs { get; set; }

    public int FailedJobs { get; set; }

    public int CompletedJobs { get; set; }

    public double AverageConsistencyScore { get; set; }

    public decimal EstimatedCost { get; set; }
}
