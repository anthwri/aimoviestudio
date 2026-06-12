namespace Domain.Continuity;

public sealed class FilmExecutionRun
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FilmId { get; set; }

    public string Status { get; set; } = "Running";

    public int TotalScenes { get; set; }

    public int CompletedScenes { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }
}
