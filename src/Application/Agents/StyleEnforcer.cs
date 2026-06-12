using Domain.Agents;

namespace Application.Agents;

public sealed class StyleEnforcer
{
    public string Apply(StyleMemory style, string prompt)
    {
        return $""{prompt}, {style.CharacterStyle}, {style.EnvironmentStyle}, {style.LightingRules}, {style.CameraRules}"";
    }

    public bool ViolatesStyle(StyleMemory style, string outputHint)
    {
        return outputHint.Contains(""deformed"") ||
               outputHint.Contains(""extra limb"");
    }
}
