using Domain.Execution;

namespace Application.Execution;

public sealed class RenderJobProcessor
{
    public async Task ProcessAsync(RenderJob job)
    {
        job.Status = JobStatus.Running;

        try
        {
            // Simulate render
            await Task.Delay(50);

            job.Status = JobStatus.Complete;
        }
        catch
        {
            job.Status = JobStatus.Failed;
            job.RetryCount++;
        }
    }
}
