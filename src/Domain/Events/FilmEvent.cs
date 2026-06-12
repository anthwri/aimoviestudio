namespace Domain.Events;

public sealed class FilmEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FilmId { get; set; }

    public Guid? SceneId { get; set; }

    public Guid? ShotId { get; set; }

    public string EventType { get; set; } = "";

    public string Message { get; set; } = "";

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
