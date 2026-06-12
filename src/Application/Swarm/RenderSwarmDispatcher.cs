using Domain.Swarm;
using Infrastructure.Media;

namespace Application.Swarm;

public sealed class RenderSwarmDispatcher
{
    private readonly GpuSwarmRegistry _registry;
    private readonly IImageGenerator _generator;

    public RenderSwarmDispatcher(GpuSwarmRegistry registry, IImageGenerator generator)
    {
        _registry = registry;
        _generator = generator;
    }

    public async Task<RenderTask> DispatchAsync(RenderTask task)
    {
        var node = _registry.GetLeastLoadedNode();

        if (node == null)
            throw new Exception(""No GPU nodes available"");

        task.AssignedNode = node.NodeId;
        task.Status = ""running"";

        node.CurrentLoad++;

        var result = await _generator.GenerateAsync(task.Prompt);

        task.Status = ""complete"";

        node.CurrentLoad--;

        return task;
    }
}
