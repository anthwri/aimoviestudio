namespace Application.Vision;

public interface IEmbeddingRepository
{
    Task StoreCharacterEmbedding(string characterId, float[] vector);

    Task StoreFrameEmbedding(string frameId, float[] vector);

    Task<float[]?> GetCharacterEmbedding(string characterId);
}
