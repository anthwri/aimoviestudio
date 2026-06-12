namespace Domain.Movies;

public sealed class CharacterIdentity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string Wardrobe { get; set; } = "";

    // Core consistency anchors
    public string Hair { get; set; } = "";
    public string FaceDescription { get; set; } = "";
    public string BodyType { get; set; } = "";

    // AI consistency tools
    public string LoraModelPath { get; set; } = "";
    public string ReferenceImagePath { get; set; } = "";
}
