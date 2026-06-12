using Domain.Production;

namespace Application.Cloud;

public sealed class CloudBurstScaler
{
    private readonly CloudProvisioningService _provisioner;
    private readonly CloudCostTracker _costs;

    public CloudBurstScaler(
        CloudProvisioningService provisioner,
        CloudCostTracker costs)
    {
        _provisioner = provisioner;
        _costs = costs;
    }

    public void ScaleUpIfNeeded(int queuedJobs, int activeNodes)
    {
        if (queuedJobs > activeNodes * 5)
        {
            _provisioner.Provision(""RunPod"");
        }
    }

    public void ScaleDownIfIdle(int queuedJobs, List<CloudGpuInstance> nodes)
    {
        if (queuedJobs == 0)
        {
            foreach (var node in nodes)
            {
                _provisioner.Shutdown(node);
            }
        }
    }
}
