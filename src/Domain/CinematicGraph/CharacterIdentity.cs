namespace Domain.CinematicGraph;

public sealed class CharacterIdentity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "";

    public string VisualDescription { get; set; } = "";

    public string ClothingStyle { get; set; } = "";

    public string VoiceTone { get; set; } = "";

    public string StableEmbeddingKey { get; set; } = "";
}
