namespace Domain.KnowledgeGraph;

public sealed class GraphEdge
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SourceId { get; set; }

    public Guid TargetId { get; set; }

    public string Relationship { get; set; } = "";
}
