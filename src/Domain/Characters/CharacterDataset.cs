namespace Domain.Movies;

public sealed class CharacterDataset
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CharacterId { get; set; }

    public List<string> ImagePaths { get; set; } = new();

    public string CaptionTemplate { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
