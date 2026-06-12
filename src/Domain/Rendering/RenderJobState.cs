namespace Domain.Rendering;

public enum RenderState
{
    Queued,
    Generating,
    Validating,
    Approved,
    Rejected,
    Archived
}

public sealed class RenderJobState
{
    public Guid JobId { get; set; } = Guid.NewGuid();
    public string Prompt { get; set; } = "";
    public RenderState State { get; set; } = RenderState.Queued;
    public string OutputPath { get; set; } = "";
}
