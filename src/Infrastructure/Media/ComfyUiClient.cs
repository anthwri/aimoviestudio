namespace Infrastructure.Media;

public sealed class ComfyUiClient : IImageGenerator
{
    public async Task<string> GenerateAsync(string prompt)
    {
        // placeholder for real HTTP call to ComfyUI API
        await Task.Delay(500);

        return $"comfyui://generated/{Guid.NewGuid()}";
    }
}

public sealed class StableDiffusionClient : IImageGenerator
{
    public async Task<string> GenerateAsync(string prompt)
    {
        await Task.Delay(500);

        return $"sd://image/{Guid.NewGuid()}";
    }
}
