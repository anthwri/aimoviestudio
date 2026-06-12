using Infrastructure.Media;
using Domain.Rendering;

namespace Application.Media;

public sealed class MediaOrchestrator
{
    private readonly IImageGenerator _images;
    private readonly IVideoGenerator _video;

    public MediaOrchestrator(IImageGenerator images, IVideoGenerator video)
    {
        _images = images;
        _video = video;
    }

    public async Task<RenderJobState> RenderShotAsync(RenderJobState job)
    {
        job.State = RenderState.Generating;

        var image = await _images.GenerateAsync(job.Prompt);

        job.OutputPath = image; // placeholder for real asset storage

        job.State = RenderState.Validating;

        if (image.Contains("error") || image.Contains("bad"))
        {
            job.State = RenderState.Rejected;
            job.Prompt += ", retry with higher quality, fix artifacts";
            return job;
        }

        job.State = RenderState.Approved;
        return job;
    }
}
