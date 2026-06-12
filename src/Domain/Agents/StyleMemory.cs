namespace Domain.Agents;

public sealed class StyleMemory
{
    public Guid FilmId { get; set; }

    public string CharacterStyle { get; set; } = "";

    public string EnvironmentStyle { get; set; } = "";

    public string LightingRules { get; set; } = "";

    public string CameraRules { get; set; } = "";

    public string ForbiddenElements { get; set; } = "";
}
