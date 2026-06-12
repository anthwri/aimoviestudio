using Application.Jobs;
using Domain.Jobs;

namespace Application.Orchestration;

public sealed class RenderingOrchestrator
{
    private readonly RenderJobQueue _queue;
    private readonly RenderJobService _service;

    public RenderingOrchestrator(
        RenderJobQueue queue,
        RenderJobService service)
    {
        _queue = queue;
        _service = service;
    }

    public void Start()
    {
        _queue.StartWorkers(
            workerCount: 3,
            handler: async job => await _service.ExecuteAsync(job));
    }

    public void Submit(RenderJob job)
    {
        _queue.Enqueue(job);
    }
}
