namespace Application.Executive;

public sealed class SequelPrioritisationEngine
{
    public string DetermineNextProject(List<string> canonEvents)
    {
        if (canonEvents.Any(e => e.Contains(""cliffhanger"")))
            return ""Direct Sequel Priority"";

        if (canonEvents.Any(e => e.Contains(""popular character"")))
            return ""Character Spin-off Priority"";

        if (canonEvents.Count > 10)
            return ""Universe Expansion Film"";

        return ""New Concept Development"";
    }
}
