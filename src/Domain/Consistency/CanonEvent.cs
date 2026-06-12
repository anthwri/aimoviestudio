namespace Domain.Consistency;

public sealed class CanonEvent
{
    public string Id { get; set; } = "";

    public int TimelineOrder { get; set; }

    public string Description { get; set; } = "";

    public List<string> Participants { get; set; }
        = new();
}
