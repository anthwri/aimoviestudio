namespace Domain.Cloud;

public sealed class CloudGpuInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Provider { get; set; } = "RunPod"; // or AWS, etc

    public string Region { get; set; } = "us-east-1";

    public string Endpoint { get; set; } = "";

    public bool IsActive { get; set; } = false;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public decimal CostPerSecond { get; set; } = 0.0003m;
}
