namespace Domain.KnowledgeGraph;

public sealed class GraphNode
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Type { get; set; } = "";

    public string Name { get; set; } = "";

    public Dictionary<string,string> Properties { get; set; } = new();
}
