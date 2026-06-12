namespace Domain.MemoryFusion;

public sealed class CinematicMemoryPacket
{
    public string EntityId { get; set; } = "";

    public string EntityType { get; set; } = "";

    public string NarrativeSummary { get; set; } = "";

    public float[]? VectorEmbedding { get; set; }

    public string? GraphContext { get; set; }

    public string? VisualContext { get; set; }

    public double ConfidenceScore { get; set; }
}
