using Domain.Execution;

namespace Application.Execution;

public sealed class SceneExecutionPlanner
{
    public IEnumerable<RenderJob> CreateJobs(string sceneId, int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return new RenderJob
            {
                Id = $"{sceneId}_frame_{i}",
                Type = "frame_render",
                Payload = $"Scene:{sceneId};Frame:{i}"
            };
        }
    }
}
