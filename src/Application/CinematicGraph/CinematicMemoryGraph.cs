using Domain.CinematicGraph;

namespace Application.CinematicGraph;

public sealed class CinematicMemoryGraph
{
    private readonly List<FilmNode> _films = new();
    private readonly List<ActNode> _acts = new();
    private readonly List<SceneNode> _scenes = new();
    private readonly List<ShotNode> _shots = new();

    public FilmNode CreateFilm(string title)
    {
        var film = new FilmNode { Title = title };
        _films.Add(film);
        return film;
    }

    public ActNode AddAct(Guid filmId, string name)
    {
        var act = new ActNode { FilmId = filmId, Name = name };
        _acts.Add(act);
        return act;
    }

    public SceneNode AddScene(Guid actId, string desc, string location, string time)
    {
        var scene = new SceneNode
        {
            ActId = actId,
            Description = desc,
            Location = location,
            TimeOfDay = time
        };

        _scenes.Add(scene);
        return scene;
    }

    public ShotNode AddShot(Guid sceneId, string prompt, string angle)
    {
        var shot = new ShotNode
        {
            SceneId = sceneId,
            Prompt = prompt,
            CameraAngle = angle
        };

        _shots.Add(shot);
        return shot;
    }

    public List<SceneNode> GetScenes(Guid actId)
    {
        return _scenes.Where(s => s.ActId == actId).ToList();
    }
}
