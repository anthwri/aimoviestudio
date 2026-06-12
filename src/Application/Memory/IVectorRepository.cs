namespace Application.Memory;

public interface IVectorRepository
{
    Task StoreEmbeddingAsync(
        string id,
        float[] vector);

    Task<IEnumerable<string>> SearchAsync(
        float[] vector,
        int topK);
}
