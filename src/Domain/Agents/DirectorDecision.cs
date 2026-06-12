namespace Domain.Agents;

public sealed class DirectorDecision
{
    public Guid FilmId { get; set; }

    public string Action { get; set; } = ""; 
    // e.g. "REWRITE_SCENE", "ADJUST_SHOT", "CONTINUE"

    public string Reason { get; set; } = "";

    public string? ModifiedPrompt { get; set; }

    public double Confidence { get; set; }
}
