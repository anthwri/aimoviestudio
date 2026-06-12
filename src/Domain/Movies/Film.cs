namespace Domain.Movies;

public sealed class Film
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = "";

    public string Idea { get; set; } = "";

    public List<Movie> Movies { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
