using Application.Vision;

namespace Infrastructure.Vision;

public sealed class ClipEmbeddingService : IVisionEmbeddingService
{
    public async Task<float[]> GetEmbeddingAsync(string imagePath)
    {
        // REAL IMPLEMENTATION POINT:
        // Call Python CLIP / SigLIP service OR ONNX runtime

        await Task.Delay(50);

        // placeholder until model is wired
        return new float[512];
    }
}
