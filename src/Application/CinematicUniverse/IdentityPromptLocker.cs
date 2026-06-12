using Domain.CinematicUniverse;

namespace Application.CinematicUniverse;

public sealed class IdentityPromptLocker
{
    public string Apply(CharacterVisualAnchor anchor, string prompt)
    {
        return $"{prompt}, character: {anchor.BasePrompt}, style lock: {anchor.StyleLock}, consistent face, same identity, same person across all frames";
    }
}
