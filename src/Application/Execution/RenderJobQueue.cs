using Domain.Execution;

namespace Application.Execution;

public sealed class RenderJobQueue
{
    private readonly Queue<RenderJob> _queue = new();

    public void Enqueue(RenderJob job)
    {
        _queue.Enqueue(job);
    }

    public RenderJob? Dequeue()
    {
        if (_queue.Count == 0)
            return null;

        return _queue.Dequeue();
    }

    public int Count => _queue.Count;
}
