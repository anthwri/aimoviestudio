using Domain.CinematicUniverse;

namespace Application.CinematicUniverse;

public sealed class CinematicUniverseMemory
{
    private readonly Dictionary<string, List<string>> _universeMemory = new();

    public void Store(string key, string value)
    {
        if (!_universeMemory.ContainsKey(key))
            _universeMemory[key] = new List<string>();

        _universeMemory[key].Add(value);
    }

    public string Recall(string key)
    {
        if (!_universeMemory.ContainsKey(key))
            return string.Empty;

        return string.Join(" | ", _universeMemory[key].TakeLast(20));
    }

    public void StoreCharacterEvent(string characterName, string eventDescription)
    {
        Store($""character:{characterName}"", eventDescription);
    }

    public void StoreWorldState(string scene, string state)
    {
        Store($""world:{scene}"", state);
    }
}
