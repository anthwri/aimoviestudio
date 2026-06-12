using Application.Infrastructure;
using Domain.Jobs;

namespace Application.Jobs;

public sealed class RenderLoadBalancer
{
    private readonly RenderNodeRegistry _registry;

    public RenderLoadBalancer(RenderNodeRegistry registry)
    {
        _registry = registry;
    }

    public RenderNode? SelectNode(RenderJob job)
    {
        // Simple heuristic:
        // later we replace this with ML-based routing

        var node = _registry.GetLeastLoadedNode();

        if (node != null)
        {
            node.ActiveJobs++;
        }

        return node;
    }
}
