using Domain.Jobs;
using Domain.Production;
using Application.Infrastructure;

namespace Application.Production;

public sealed class IntelligentScheduler
{
    private readonly JobScoringEngine _scorer;
    private readonly RenderNodeRegistry _nodes;

    public IntelligentScheduler(
        JobScoringEngine scorer,
        RenderNodeRegistry nodes)
    {
        _scorer = scorer;
        _nodes = nodes;
    }

    public RenderNode? SelectBestNode(
        RenderJob job,
        ProductionJobProfile profile)
    {
        var nodes = _nodes.GetHealthyNodes();

        if (!nodes.Any())
            return null;

        // Prefer least loaded + most stable
        return nodes
            .OrderBy(n => n.ActiveJobs)
            .ThenByDescending(n => n.IsHealthy)
            .FirstOrDefault();
    }

    public List<RenderJob> Prioritize(
        List<RenderJob> jobs,
        ProductionJobProfile profile)
    {
        return jobs
            .OrderByDescending(j => _scorer.Score(j, profile))
            .ToList();
    }
}
