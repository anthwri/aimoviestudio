namespace Domain.Identity;

public sealed class CharacterIdentityProfile
{
    public string CharacterId { get; set; } = "";

    public string Name { get; set; } = "";

    public string Biography { get; set; } = "";

    public string Personality { get; set; } = "";

    public string VoiceDescription { get; set; } = "";

    public string AppearanceDescription { get; set; } = "";

    public List<string> ReferenceImages { get; set; }
        = new();

    public List<string> AppearanceRules { get; set; }
        = new();
}
