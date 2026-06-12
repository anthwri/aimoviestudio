namespace Domain.Continuity;

public sealed class ContinuityRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid MovieId { get; set; }

    public Guid SceneId { get; set; }

    public string Type { get; set; } = ""; // Character / Prop / Location

    public string EntityId { get; set; } = "";

    public string StateJson { get; set; } = "";
}
