using Domain.Cloud;

namespace Application.Cloud;

public sealed class CloudProvisioningService
{
    private readonly List<CloudGpuInstance> _instances = new();

    public CloudGpuInstance Provision(string provider = "RunPod")
    {
        var instance = new CloudGpuInstance
        {
            Provider = provider,
            IsActive = true,
            Endpoint = $"https://gpu-{Guid.NewGuid()}.cloud.local"
        };

        _instances.Add(instance);

        return instance;
    }

    public void Shutdown(CloudGpuInstance instance)
    {
        instance.IsActive = false;
    }

    public List<CloudGpuInstance> GetActive() =>
        _instances.Where(i => i.IsActive).ToList();
}
