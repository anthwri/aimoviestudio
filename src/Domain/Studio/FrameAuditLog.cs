namespace Domain.Studio;

public sealed class FrameAuditLog
{
    public string FrameId { get; set; } = "";

    public double CharacterScore { get; set; }

    public double EnvironmentScore { get; set; }

    public double PropScore { get; set; }

    public double OverallScore { get; set; }

    public string FailureReason { get; set; } = "";

    public bool Rejected { get; set; }
}
