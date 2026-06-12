namespace Domain.MemoryFusion;

public sealed class MemoryFusionRequest
{
    public string CharacterId { get; set; } = "";

    public string? EnvironmentId { get; set; }

    public string Query { get; set; } = "";
}
