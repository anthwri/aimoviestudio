using Application.Jobs;
using Domain.Jobs;

namespace Application.Orchestration;

public sealed class ShotRerenderService
{
    private readonly RenderingOrchestrator _orchestrator;

    public ShotRerenderService(RenderingOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public void Rerender(RenderJob original)
    {
        var job = new RenderJob
        {
            FilmId = original.FilmId,
            SceneId = original.SceneId,
            ShotId = original.ShotId,
            Prompt = original.Prompt,
            NegativePrompt = original.NegativePrompt
        };

        _orchestrator.Submit(job);
    }
}
