using Domain.Consistency;

namespace Application.Consistency;

public sealed class CharacterConsistencyBuilder
{
    public string Build(
        AppearanceLock appearance,
        WardrobeProfile wardrobe)
    {
        return $"""
CHARACTER LOCK

Hair:
{appearance.HairColor}
{appearance.HairStyle}

Eyes:
{appearance.EyeColor}

Face:
{appearance.FaceDescription}

Body:
{appearance.BodyDescription}

Wardrobe:
{wardrobe.Description}

DO NOT CHANGE THESE ATTRIBUTES
""";
    }
}
