using Domain.CinematicGraph;
using Domain.CinematicUniverse;
using Domain.Franchise;

namespace Infrastructure.Persistence;

public sealed class CinematicGraphStore
{
    private readonly List<FilmNode> _films = new();
    private readonly List<SceneNode> _scenes = new();
    private readonly List<ShotNode> _shots = new();
    private readonly List<CharacterVisualAnchor> _characters = new();

    public void SaveFilm(FilmNode film) => _films.Add(film);
    public void SaveScene(SceneNode scene) => _scenes.Add(scene);
    public void SaveShot(ShotNode shot) => _shots.Add(shot);
    public void SaveCharacter(CharacterVisualAnchor character) => _characters.Add(character);

    public IEnumerable<FilmNode> GetFilms() => _films;
    public IEnumerable<SceneNode> GetScenes() => _scenes;
    public IEnumerable<ShotNode> GetShots() => _shots;
}
