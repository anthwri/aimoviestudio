namespace Domain.Movies;

public sealed class LoraTrainingResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid JobId { get; set; }

    public string ModelPath { get; set; } = "";

    public string CharacterName { get; set; } = "";

    public TrainingStatus Status { get; set; }

    public string? Logs { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}
