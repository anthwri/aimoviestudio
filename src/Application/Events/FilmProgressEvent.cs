namespace Application.Events;

public sealed class FilmProgressEvent
{
    public Guid FilmId { get; set; }

    public Guid? SceneId { get; set; }

    public Guid? ShotId { get; set; }

    public string Status { get; set; } = "";

    public string Message { get; set; } = "";

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
