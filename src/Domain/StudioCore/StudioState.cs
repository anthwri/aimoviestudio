namespace Domain.StudioCore;

public sealed class StudioState
{
    public Guid StudioId { get; set; } = Guid.NewGuid();

    public string UniverseMemory { get; set; } = "";

    public string ActiveProjects { get; set; } = "";

    public string CanonHistory { get; set; } = "";

    public string LearningState { get; set; } = "";

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
