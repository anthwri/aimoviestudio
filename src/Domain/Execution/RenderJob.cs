namespace Domain.Execution;

public sealed class RenderJob
{
    public string Id { get; set; } = "";

    public string Type { get; set; } = "";

    public string Payload { get; set; } = "";

    public int RetryCount { get; set; }

    public JobStatus Status { get; set; } = JobStatus.Pending;

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
