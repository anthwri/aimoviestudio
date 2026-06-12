using Application.Environment;
using Application.Identity;

namespace Application.Prompting;

public sealed class ConsistencyContextBuilder
{
    private readonly CharacterIdentityService _characters;
    private readonly EnvironmentMemoryService _environments;

    public ConsistencyContextBuilder(
        CharacterIdentityService characters,
        EnvironmentMemoryService environments)
    {
        _characters = characters;
        _environments = environments;
    }

    public string Build(
        string characterId,
        string environmentId)
    {
        var character =
            _characters.Get(characterId);

        var environment =
            _environments.Get(environmentId);

        return $"""
CHARACTER

Name:
{character?.Name}

Appearance:
{character?.AppearanceDescription}

Rules:
{string.Join(",", character?.AppearanceRules ?? new())}

ENVIRONMENT

Name:
{environment?.Name}

Layout:
{environment?.LayoutDescription}

Lighting:
{environment?.LightingProfile}

Objects:
{string.Join(",", environment?.Objects ?? new())}
""";
    }
}
