namespace Application.Agents;

public sealed class StudioMemory
{
    private readonly Dictionary<string, List<string>> _memory = new();

    public void Store(string key, string value)
    {
        if (!_memory.ContainsKey(key))
            _memory[key] = new List<string>();

        _memory[key].Add(value);
    }

    public string GetContext(string key)
    {
        if (!_memory.ContainsKey(key)) return """";

        return string.Join(""\n"", _memory[key].TakeLast(10));
    }
}
