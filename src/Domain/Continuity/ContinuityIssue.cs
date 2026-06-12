namespace Domain.Continuity;

public sealed class ContinuityIssue
{
    public string Type { get; set; } = ""; // error, warning
    public string Message { get; set; } = "";
    public Guid SceneId { get; set; }
}
