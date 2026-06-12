namespace Infrastructure.Swarm;

public sealed class GpuNode
{
    public Guid NodeId { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "";

    public int AvailableVRAM { get; set; }

    public bool IsOnline { get; set; } = true;

    public int CurrentLoad { get; set; } = 0;
}
