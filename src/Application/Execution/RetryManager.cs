using Domain.Execution;

namespace Application.Execution;

public sealed class RetryManager
{
    private const int MaxRetries = 3;

    public bool ShouldRetry(RenderJob job)
    {
        return job.Status == JobStatus.Failed &&
               job.RetryCount < MaxRetries;
    }

    public RenderJob Retry(RenderJob job)
    {
        job.Status = JobStatus.Retrying;
        return job;
    }
}
