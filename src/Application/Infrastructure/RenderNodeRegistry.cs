using Domain.Infrastructure;

namespace Application.Infrastructure;

public sealed class RenderNodeRegistry
{
    private readonly List<RenderNode> _nodes = new();

    public void Register(RenderNode node)
    {
        _nodes.Add(node);
    }

    public List<RenderNode> GetHealthyNodes()
    {
        return _nodes
            .Where(n => n.IsHealthy && n.ActiveJobs < n.MaxCapacity)
            .ToList();
    }

    public RenderNode? GetLeastLoadedNode()
    {
        return GetHealthyNodes()
            .OrderBy(n => n.ActiveJobs)
            .FirstOrDefault();
    }
}
