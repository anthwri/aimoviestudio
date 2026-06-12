namespace Domain.VectorMemory;

public sealed class VectorRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string EntityType { get; set; } = "";

    public string EntityId { get; set; } = "";

    public float[] Embedding { get; set; } = Array.Empty<float>();
}
