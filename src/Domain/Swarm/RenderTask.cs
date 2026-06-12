namespace Domain.Swarm;

public sealed class RenderTask
{
    public Guid TaskId { get; set; } = Guid.NewGuid();

    public string Prompt { get; set; } = "";

    public string Backend { get; set; } = "comfyui";

    public string Status { get; set; } = "queued";

    public Guid AssignedNode { get; set; }
}
