using Application.Vision;

namespace Application.Vision;

public sealed class EmbeddingPipeline
{
    private readonly IVisionEmbeddingService _vision;
    private readonly IEmbeddingRepository _repo;

    public EmbeddingPipeline(
        IVisionEmbeddingService vision,
        IEmbeddingRepository repo)
    {
        _vision = vision;
        _repo = repo;
    }

    public async Task<float[]> ProcessCharacterImage(
        string characterId,
        string imagePath)
    {
        var embedding =
            await _vision.GetEmbeddingAsync(imagePath);

        await _repo.StoreCharacterEmbedding(
            characterId,
            embedding);

        return embedding;
    }

    public async Task<float[]> ProcessFrame(
        string frameId,
        string imagePath)
    {
        var embedding =
            await _vision.GetEmbeddingAsync(imagePath);

        await _repo.StoreFrameEmbedding(
            frameId,
            embedding);

        return embedding;
    }
}
