using Domain.Franchise;

namespace Application.Franchise;

public sealed class FranchiseMemory
{
    private readonly Dictionary<Guid, UniverseNode> _universes = new();
    private readonly List<FilmNode> _films = new();
    private readonly List<CanonEvent> _canon = new();

    public UniverseNode CreateUniverse(string name)
    {
        var universe = new UniverseNode { Name = name };
        _universes[universe.Id] = universe;
        return universe;
    }

    public FilmNode AddFilm(Guid universeId, string title, int order)
    {
        var film = new FilmNode
        {
            UniverseId = universeId,
            Title = title,
            ChronologicalOrder = order
        };

        _films.Add(film);
        return film;
    }

    public void AddCanonEvent(Guid universeId, string description)
    {
        _canon.Add(new CanonEvent
        {
            UniverseId = universeId,
            Description = description
        });
    }

    public List<CanonEvent> GetCanon(Guid universeId)
    {
        return _canon.Where(c => c.UniverseId == universeId).ToList();
    }
}
