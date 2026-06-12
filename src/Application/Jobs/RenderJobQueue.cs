using System.Collections.Concurrent;
using Domain.Jobs;

namespace Application.Jobs;

public sealed class RenderJobQueue
{
    private readonly ConcurrentQueue<RenderJob> _queue = new();
    private readonly SemaphoreSlim _signal = new(0);
    private readonly List<Task> _workers = new();

    public void Enqueue(RenderJob job)
    {
        _queue.Enqueue(job);
        _signal.Release();
    }

    public void StartWorkers(int workerCount, Func<RenderJob, Task> handler)
    {
        for (int i = 0; i < workerCount; i++)
        {
            _workers.Add(Task.Run(async () =>
            {
                while (true)
                {
                    await _signal.WaitAsync();

                    if (_queue.TryDequeue(out var job))
                    {
                        try
                        {
                            job.Status = JobStatus.Running;
                            await handler(job);
                            job.Status = JobStatus.Completed;
                        }
                        catch
                        {
                            job.Status = JobStatus.Failed;
                        }
                    }
                }
            }));
        }
    }
}
