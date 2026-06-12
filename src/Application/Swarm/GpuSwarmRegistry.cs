using Infrastructure.Swarm;

namespace Application.Swarm;

public sealed class GpuSwarmRegistry
{
    private readonly List<GpuNode> _nodes = new();

    public void RegisterNode(string name, int vram)
    {
        _nodes.Add(new GpuNode
        {
            Name = name,
            AvailableVRAM = vram
        });
    }

    public GpuNode? GetLeastLoadedNode()
    {
        return _nodes
            .Where(n => n.IsOnline)
            .OrderBy(n => n.CurrentLoad)
            .FirstOrDefault();
    }

    public IEnumerable<GpuNode> GetAllNodes() => _nodes;
}
