using Domain.Jobs;
using Application.Events;
using Infrastructure.ComfyUI;

namespace Application.Jobs;

public sealed class RenderJobService
{
    private readonly ComfyUiClient _comfy;
    private readonly FilmEventBroadcaster _events;

    public RenderJobService(
        ComfyUiClient comfy,
        FilmEventBroadcaster events)
    {
        _comfy = comfy;
        _events = events;
    }

    public async Task ExecuteAsync(RenderJob job)
    {
        await _events.PublishAsync(new FilmProgressEvent
        {
            FilmId = job.FilmId,
            SceneId = job.SceneId,
            ShotId = job.ShotId,
            Status = ""running"",
            Message = ""Rendering started""
        });

        try
        {
            var result = await _comfy.GenerateImageAsync(
                job.Prompt,
                job.NegativePrompt);

            job.OutputPath = $""/generated/{result}.png"";

            await _events.PublishAsync(new FilmProgressEvent
            {
                FilmId = job.FilmId,
                SceneId = job.SceneId,
                ShotId = job.ShotId,
                Status = ""completed"",
                Message = ""Shot rendered successfully""
            });
        }
        catch (Exception ex)
        {
            await _events.PublishAsync(new FilmProgressEvent
            {
                FilmId = job.FilmId,
                SceneId = job.SceneId,
                ShotId = job.ShotId,
                Status = ""failed"",
                Message = ex.Message
            });

            throw;
        }
    }
}
