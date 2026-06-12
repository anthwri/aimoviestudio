using Domain.Jobs;
using Domain.Movies;

namespace Application.Orchestration;

public sealed class FilmRenderPipeline
{
    private readonly RenderingOrchestrator _orchestrator;

    public FilmRenderPipeline(RenderingOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public void RenderFilm(Film film)
    {
        foreach (var movie in film.Movies)
        foreach (var scene in movie.Scenes)
        foreach (var shot in scene.Shots)
        {
            var job = new RenderJob
            {
                FilmId = film.Id,
                SceneId = scene.Id,
                ShotId = shot.Id,
                Prompt = shot.Prompt,
                NegativePrompt = "blurry, distorted, low quality"
            };

            _orchestrator.Submit(job);
        }
    }
}
