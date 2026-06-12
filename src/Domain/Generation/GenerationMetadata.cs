namespace Domain.Generation;

public sealed class GenerationMetadata
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Prompt { get; set; } = "";

    public string NegativePrompt { get; set; } = "";

    public long Seed { get; set; }

    public string ModelName { get; set; } = "";

    public string WorkflowJson { get; set; } = "";

    public DateTime GeneratedUtc { get; set; }
}
