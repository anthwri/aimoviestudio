namespace Domain.Movies;

public sealed class Character
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Personality { get; set; } = "";
    public string Wardrobe { get; set; } = "";
}
