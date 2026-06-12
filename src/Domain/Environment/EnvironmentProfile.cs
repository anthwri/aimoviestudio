namespace Domain.Environment;

public sealed class EnvironmentProfile
{
    public string Id { get; set; } = "";

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string LayoutDescription { get; set; } = "";

    public string LightingProfile { get; set; } = "";

    public List<string> Objects { get; set; }
        = new();

    public List<string> ReferenceImages { get; set; }
        = new();
}
