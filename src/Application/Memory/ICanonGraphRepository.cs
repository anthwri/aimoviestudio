namespace Application.Memory;

public interface ICanonGraphRepository
{
    Task SaveCharacterAsync(
        string id,
        string name);

    Task SaveSceneAsync(
        string id,
        string title);

    Task<IEnumerable<string>> GetCharacterScenesAsync(
        string characterId);
}
