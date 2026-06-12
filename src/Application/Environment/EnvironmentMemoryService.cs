using Domain.Environment;

namespace Application.Environment;

public sealed class EnvironmentMemoryService
{
    private readonly Dictionary<string,EnvironmentProfile>
        _cache = new();

    public void Register(EnvironmentProfile profile)
    {
        _cache[profile.Id] = profile;
    }

    public EnvironmentProfile? Get(string id)
    {
        _cache.TryGetValue(id, out var profile);

        return profile;
    }
}
