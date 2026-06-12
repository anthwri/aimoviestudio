namespace Domain.CinematicGraph;

public sealed class FilmNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
}

public sealed class ActNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FilmId { get; set; }
    public string Name { get; set; } = "";
}

public sealed class SceneNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ActId { get; set; }
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public string TimeOfDay { get; set; } = "";
}

public sealed class ShotNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SceneId { get; set; }
    public string Prompt { get; set; } = "";
    public string CameraAngle { get; set; } = "";
}
