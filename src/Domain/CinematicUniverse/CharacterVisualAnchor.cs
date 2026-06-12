namespace Domain.CinematicUniverse;

public sealed class CharacterVisualAnchor
{
    public Guid CharacterId { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "";

    public string BasePrompt { get; set; } = "";

    public string NegativePrompt { get; set; } = "";

    public List<string> ReferenceImages { get; set; } = new();

    public string EmbeddingKey { get; set; } = "";

    public string StyleLock { get; set; } = "cinematic realism, consistent identity";
}
