namespace Domain.Consistency;

public sealed class LocationLayoutLock
{
    public string EnvironmentId { get; set; } = "";

    public string LayoutDescription { get; set; } = "";

    public string LightingDescription { get; set; } = "";

    public List<string> PermanentObjects { get; set; }
        = new();
}
