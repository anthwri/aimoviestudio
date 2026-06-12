using Application.Vision;
using Infrastructure.Vector;

namespace Infrastructure.Vision;

public sealed class QdrantEmbeddingRepository : IEmbeddingRepository
{
    private readonly QdrantClient _client;

    public QdrantEmbeddingRepository(QdrantClient client)
    {
        _client = client;
    }

    public Task StoreCharacterEmbedding(string characterId, float[] vector)
        => _client.UpsertAsync("characters", characterId, vector);

    public Task StoreFrameEmbedding(string frameId, float[] vector)
        => _client.UpsertAsync("frames", frameId, vector);

    public Task<float[]?> GetCharacterEmbedding(string characterId)
    {
        // simplified stub (real version would query Qdrant)
        return Task.FromResult<float[]?>(null);
    }
}
