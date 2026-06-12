using Domain.Movies;

namespace Application.Characters;

public sealed class CharacterDatasetGenerator
{
    public List<string> GeneratePrompts(CharacterIdentity character)
    {
        var basePrompt =
            $"cinematic portrait of {character.Name}, {character.FaceDescription}, {character.Hair}, {character.BodyType}, {character.Wardrobe}";

        var variations = new List<string>();

        var lighting = new[]
        {
            "cinematic lighting",
            "low key lighting",
            "golden hour sunlight",
            "neon cyberpunk glow",
            "studio softbox lighting"
        };

        var angles = new[]
        {
            "close-up portrait",
            "wide shot",
            "profile view",
            "over the shoulder",
            "dramatic low angle"
        };

        var expressions = new[]
        {
            "neutral expression",
            "angry expression",
            "sad expression",
            "determined expression",
            "surprised expression"
        };

        foreach (var l in lighting)
        foreach (var a in angles)
        foreach (var e in expressions)
        {
            variations.Add($"{basePrompt}, {a}, {e}, {l}");
        }

        return variations;
    }
}
