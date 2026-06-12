namespace Application.Execution;

public sealed class GpuScheduler
{
    private readonly SemaphoreSlim _semaphore;

    public GpuScheduler(int maxConcurrency = 2)
    {
        _semaphore = new SemaphoreSlim(maxConcurrency);
    }

    public async Task RunAsync(Func<Task> job)
    {
        await _semaphore.WaitAsync();

        try
        {
            await job();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
