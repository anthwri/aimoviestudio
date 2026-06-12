using Domain.Identity;

namespace Application.Identity;

public sealed class CharacterIdentityService
{
    private readonly Dictionary<string,CharacterIdentityProfile>
        _profiles = new();

    public void Register(CharacterIdentityProfile profile)
    {
        _profiles[profile.CharacterId] = profile;
    }

    public CharacterIdentityProfile? Get(string id)
    {
        _profiles.TryGetValue(id, out var profile);

        return profile;
    }
}
