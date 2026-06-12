using Domain.Execution;

namespace Application.Execution;

public sealed class ExecutionOrchestrator
{
    private readonly RenderJobQueue _queue;
    private readonly RenderJobProcessor _processor;
    private readonly RetryManager _retry;

    public ExecutionOrchestrator(
        RenderJobQueue queue,
        RenderJobProcessor processor,
        RetryManager retry)
    {
        _queue = queue;
        _processor = processor;
        _retry = retry;
    }

    public async Task TickAsync()
    {
        var job = _queue.Dequeue();

        if (job == null)
            return;

        await _processor.ProcessAsync(job);

        if (_retry.ShouldRetry(job))
        {
            _queue.Enqueue(_retry.Retry(job));
        }
    }
}
