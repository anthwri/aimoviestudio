using Agents.Director;
using Agents.Storyboard;
using Application.Storyboard;
using Application.Characters;
using Contracts.Director;
using Domain.Movies;

namespace Application.Orchestration;

public sealed class FilmOrchestrator
{
    private readonly DirectorAgent _director;
    private readonly StoryboardAgent _storyboard;
    private readonly CharacterAwareRenderService _renderer;

    public FilmOrchestrator(
        DirectorAgent director,
        StoryboardAgent storyboard,
        CharacterAwareRenderService renderer)
    {
        _director = director;
        _storyboard = storyboard;
        _renderer = renderer;
    }

    public async Task<FilmResponse> CreateFilmAsync(
        CreateFilmRequest request,
        CancellationToken ct)
    {
        // 1. Generate movie structure
        var movie = await _director.GenerateAsync(request.Idea, ct);

        var result = new FilmResponse
        {
            Title = movie.Title,
            MovieId = Guid.NewGuid()
        };

        int sceneIndex = 1;

        foreach (var scene in movie.Scenes.Take(request.MaxScenes))
        {
            var sceneResult = new SceneResult
            {
                SceneNumber = sceneIndex
            };

            int shotIndex = 1;

            foreach (var shot in movie.Shots.Where(s => s.SceneId == Guid.Empty))
            {
                var storyboard = _storyboard.Generate(
                    new Movie(), scene, shot);

                var imageId = "";

                if (request.GenerateImages)
                {
                    imageId = await _renderer.RenderCharacterLockedAsync(
                        storyboard.Prompt,
                        storyboard.NegativePrompt,
                        movie.Characters.First(),
                        ct);
                }

                sceneResult.Shots.Add(new ShotResult
                {
                    ShotNumber = shotIndex,
                    Prompt = storyboard.Prompt,
                    ImagePath = imageId
                });

                shotIndex++;
            }

            result.Scenes.Add(sceneResult);
            sceneIndex++;
        }

        return result;
    }
}
