namespace Domain.Studio;

public sealed class MemorySnapshot
{
    public string EntityId { get; set; } = "";

    public string Type { get; set; } = "";

    public string Summary { get; set; } = "";

    public double Confidence { get; set; }
}
