using Domain.Jobs;
using Application.Infrastructure;
using Infrastructure.ComfyUI;

namespace Application.Jobs;

public sealed class DistributedRenderService
{
    private readonly RenderNodeRegistry _registry;
    private readonly ComfyUiClient _comfy;

    public DistributedRenderService(
        RenderNodeRegistry registry,
        ComfyUiClient comfy)
    {
        _registry = registry;
        _comfy = comfy;
    }

    public async Task ExecuteAsync(RenderJob job)
    {
        var node = _registry.GetLeastLoadedNode();

        if (node == null)
            throw new Exception(""No render nodes available"");

        node.ActiveJobs++;

        try
        {
            // Route request to specific GPU node
            var client = new ComfyUiClient(
                new HttpClient { BaseAddress = new Uri(node.BaseUrl) });

            var result = await client.GenerateImageAsync(
                job.Prompt,
                job.NegativePrompt);

            job.OutputPath = $""{node.Name}/generated/{result}.png"";
        }
        finally
        {
            node.ActiveJobs--;
        }
    }
}
