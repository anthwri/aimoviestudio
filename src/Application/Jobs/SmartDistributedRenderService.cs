using Domain.Jobs;
using Domain.Jobs;
using Domain.Production;
using Application.Infrastructure;
using Application.Production;
using Infrastructure.ComfyUI;
using Infrastructure.Media;

namespace Application.Jobs;

public sealed class SmartDistributedRenderService
{
    private readonly IntelligentScheduler _scheduler;
    private readonly RenderNodeRegistry _nodes;

    public SmartDistributedRenderService(
        IntelligentScheduler scheduler,
        RenderNodeRegistry nodes)
    {
        _scheduler = scheduler;
        _nodes = nodes;
    }

    public async Task ExecuteAsync(
        RenderJob job,
        ProductionJobProfile profile)
    {
        var node = _scheduler.SelectBestNode(job, profile);

        if (node is null)
            throw new Exception("No available render nodes");

        node.ActiveJobs++;

        try
        {
            var client = new ComfyUiClient(
                new HttpClient { BaseAddress = new Uri(node.BaseUrl) });

            await client.GenerateAsync(job.Prompt);
        }
        finally
        {
            node.ActiveJobs--;
        }
    }
}
