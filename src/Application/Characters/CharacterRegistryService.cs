using Domain.Movies;

namespace Application.Characters;

public sealed class CharacterRegistryService
{
    public string BuildConsistencyBlock(CharacterIdentity character)
    {
        return $@"
CHARACTER LOCK:
Name: {character.Name}
Face: {character.FaceDescription}
Hair: {character.Hair}
Body: {character.BodyType}
Wardrobe: {character.Wardrobe}

STRICT RULE:
- Do NOT change facial features
- Do NOT change hairstyle
- Do NOT change body proportions
- Always match reference image
";
    }
}
