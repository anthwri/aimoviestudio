namespace Domain.Canon;

public sealed class CanonScene
{
    public string Id { get; set; } = "";

    public string Title { get; set; } = "";

    public List<string> CharacterIds { get; set; }
        = new();
}
