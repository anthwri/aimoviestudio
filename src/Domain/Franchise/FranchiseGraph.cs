namespace Domain.Franchise;

public sealed class UniverseNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
}

public sealed class FilmNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UniverseId { get; set; }
    public string Title { get; set; } = "";
    public int ChronologicalOrder { get; set; }
}

public sealed class CanonEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UniverseId { get; set; }
    public string Description { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
