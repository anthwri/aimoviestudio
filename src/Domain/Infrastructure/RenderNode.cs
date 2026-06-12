namespace Domain.Infrastructure;

public sealed class RenderNode
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "";

    public string BaseUrl { get; set; } = "";

    public bool IsHealthy { get; set; } = true;

    public int ActiveJobs { get; set; } = 0;

    public int MaxCapacity { get; set; } = 3;

    public DateTime LastHeartbeat { get; set; } = DateTime.UtcNow;
}
