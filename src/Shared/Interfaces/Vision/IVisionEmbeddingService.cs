namespace Application.Vision;

public interface IVisionEmbeddingService
{
    Task<float[]> GetEmbeddingAsync(string imagePath);
}
