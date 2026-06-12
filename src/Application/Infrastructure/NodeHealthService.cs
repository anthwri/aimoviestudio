namespace Application.Infrastructure;

public sealed class NodeHealthService
{
    private readonly RenderNodeRegistry _registry;

    public NodeHealthService(RenderNodeRegistry registry)
    {
        _registry = registry;
    }

    public void Heartbeat(string nodeName)
    {
        var node = _registry
            .GetHealthyNodes()
            .FirstOrDefault(n => n.Name == nodeName);

        if (node != null)
        {
            node.LastHeartbeat = DateTime.UtcNow;
            node.IsHealthy = true;
        }
    }
}
