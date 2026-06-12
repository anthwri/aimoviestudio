using Domain.Timeline;

namespace Application.Timeline;

public sealed class VideoAssemblyEngine
{
    public string Assemble(SceneSequence scene)
    {
        // placeholder for ffmpeg or video encoder integration

        var orderedFrames = scene.Clips
            .SelectMany(c => c.Frames)
            .OrderBy(f => f.FrameIndex)
            .ToList();

        return $""video://assembled/{scene.SceneName}/{orderedFrames.Count}_frames"";
    }
}
