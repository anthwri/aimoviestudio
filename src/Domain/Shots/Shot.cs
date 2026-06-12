namespace Domain.Movies;

public partial class Shot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SceneId { get; set; }

    public int ShotNumber { get; set; }

    public string Type { get; set; } = "";

    public string CameraDescription { get; set; } = "";

    public string Prompt { get; set; } = "";

    public string? ImagePath { get; set; }
}
