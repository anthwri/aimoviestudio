using Infrastructure.Video;
using Application.Timeline;

namespace Application.Runtime;

public sealed class CinemaRuntimeEngine
{
    private readonly FfmpegPipeline _ffmpeg;
    private readonly SceneProductionScheduler _scheduler;

    public CinemaRuntimeEngine(
        FfmpegPipeline ffmpeg,
        SceneProductionScheduler scheduler)
    {
        _ffmpeg = ffmpeg;
        _scheduler = scheduler;
    }

    public async Task<string> RunLiveFilm(string name, string prompt)
    {
        var scene = await _scheduler.ProduceSceneAsync(name, prompt);

        var video = _ffmpeg.EncodeFramesToVideo(name, $"{name}_final");

        var stream = _ffmpeg.StreamVideo(video);

        return stream;
    }
}
