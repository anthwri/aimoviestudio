namespace Domain.Jobs;

public sealed class RenderJob
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FilmId { get; set; }

    public Guid SceneId { get; set; }

    public Guid ShotId { get; set; }

    public string Prompt { get; set; } = "";

    public string NegativePrompt { get; set; } = "";

    public JobStatus Status { get; set; } = JobStatus.Queued;

    public string? OutputPath { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
