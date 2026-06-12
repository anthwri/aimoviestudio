using Domain.CinematicUniverse;
using Domain.CinematicGraph;
using FranchiseFilmNode = Domain.Franchise.FilmNode;
using CinematicFilmNode = Domain.CinematicGraph.FilmNode;

namespace Infrastructure.Persistence;

public sealed class CinematicGraphStore
{
    private readonly List<CinematicFilmNode> _films = new();
    private readonly List<SceneNode> _scenes = new();
    private readonly List<ShotNode> _shots = new();
    private readonly List<CharacterVisualAnchor> _characters = new();

    public void SaveFilm(CinematicFilmNode film) => _films.Add(film);
    public void SaveScene(SceneNode scene) => _scenes.Add(scene);
    public void SaveShot(ShotNode shot) => _shots.Add(shot);
    public void SaveCharacter(CharacterVisualAnchor character) => _characters.Add(character);

    public IEnumerable<CinematicFilmNode> GetFilms() => _films;
    public IEnumerable<SceneNode> GetScenes() => _scenes;
    public IEnumerable<ShotNode> GetShots() => _shots;
}
