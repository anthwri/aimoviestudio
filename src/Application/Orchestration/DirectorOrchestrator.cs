using Application.Agents;
using Application.Jobs;
using Domain.Jobs;

namespace Application.Orchestration;

public sealed class DirectorOrchestrator
{
    private readonly DirectorAgent _director;
    private readonly StyleEnforcer _enforcer;
    private readonly RenderingOrchestrator _renderer;

    public DirectorOrchestrator(
        DirectorAgent director,
        StyleEnforcer enforcer,
        RenderingOrchestrator renderer)
    {
        _director = director;
        _enforcer = enforcer;
        _renderer = renderer;
    }

    public async Task ExecuteShotAsync(RenderJob job, string qualityHint)
    {
        var decision = _director.EvaluateShot(
            job.FilmId,
            job.Prompt,
            qualityHint);

        if (decision.Action == "REWRITE_SCENE" && decision.ModifiedPrompt != null)
        {
            job.Prompt = decision.ModifiedPrompt;
        }

        _renderer.Submit(job);

        await Task.CompletedTask;
    }
}
