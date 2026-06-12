namespace Infrastructure.Video;

public sealed class FfmpegPipeline
{
    public string EncodeFramesToVideo(string frameFolder, string outputName)
    {
        // placeholder for ffmpeg CLI integration
        return $""video://ffmpeg/{outputName}.mp4"";
    }

    public string StreamVideo(string videoPath)
    {
        return $""stream://live/{Guid.NewGuid()}"";
    }
}
