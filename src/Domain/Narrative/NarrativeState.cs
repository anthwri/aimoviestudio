namespace Domain.Narrative;

public sealed class NarrativeState
{
    public string EmotionalGoal { get; set; } = "";

    public int ConflictLevel { get; set; }

    public string Theme { get; set; } = "";

    public string CharacterChange { get; set; } = "";

    public string AudienceTakeaway { get; set; } = "";
}
