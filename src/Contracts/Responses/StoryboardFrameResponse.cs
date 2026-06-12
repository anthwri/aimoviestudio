namespace Contracts.Director;

public sealed class StoryboardFrameResponse
{
    public int ShotNumber { get; set; }

    public string Prompt { get; set; } = "";

    public string NegativePrompt { get; set; } = "";

    public string Style { get; set; } = "";

    public string Camera { get; set; } = "";

    public string Lighting { get; set; } = "";

    public string Composition { get; set; } = "";
}
