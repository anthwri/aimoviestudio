namespace Domain.Embeddings;

public sealed class CharacterEmbedding
{
    public string CharacterId { get; set; } = "";

    public float[] Vector { get; set; }
        = Array.Empty<float>();

    public string CharacterSummary { get; set; }
        = "";
}
