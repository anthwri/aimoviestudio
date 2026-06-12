using Domain.CinematicGraph;

namespace Application.CinematicGraph;

public sealed class ContinuityEnforcer
{
    public string Enforce(SceneNode scene, CharacterIdentity character, string prompt)
    {
        return $""{prompt}, character: {character.VisualDescription}, consistent outfit: {character.ClothingStyle}, location: {scene.Location}, time: {scene.TimeOfDay}"";
    }

    public bool DetectConflict(SceneNode a, SceneNode b)
    {
        return a.Location != b.Location && a.TimeOfDay == b.TimeOfDay;
    }
}
