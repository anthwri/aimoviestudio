namespace Domain.Movies;

public sealed class CharacterModelRegistry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CharacterName { get; set; } = "";

    public string ModelPath { get; set; } = "";

    public string BaseModel { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
