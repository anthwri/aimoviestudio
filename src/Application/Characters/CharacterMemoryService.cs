using Application.Memory;

namespace Application.Characters;

public sealed class CharacterMemoryService
{
    private readonly ICanonGraphRepository _graph;

    public CharacterMemoryService(
        ICanonGraphRepository graph)
    {
        _graph = graph;
    }

    public async Task<string> BuildCharacterContext(
        string characterId)
    {
        var scenes =
            await _graph.GetCharacterScenesAsync(
                characterId);

        return string.Join(
            Environment.NewLine,
            scenes);
    }
}
