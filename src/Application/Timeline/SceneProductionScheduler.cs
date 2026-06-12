using Application.Swarm;
using Domain.Swarm;
using Domain.Timeline;

namespace Application.Timeline;

public sealed class SceneProductionScheduler
{
    private readonly RenderSwarmDispatcher _dispatcher;

    public SceneProductionScheduler(RenderSwarmDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<SceneSequence> ProduceSceneAsync(string sceneName, string prompt)
    {
        var scene = new SceneSequence { SceneName = sceneName };

        var clip = new ClipNode();

        for (int i = 0; i < 24; i++) // 24-frame clip
        {
            var task = new RenderTask
            {
                Prompt = $"{prompt}, frame {i}"
            };

            await _dispatcher.DispatchAsync(task);

            clip.Frames.Add(new FrameNode
            {
                FrameIndex = i,
                ImagePath = task.Status == "complete" ? "rendered.png" : "pending.png"
            });
        }

        scene.Clips.Add(clip);

        return scene;
    }
}
