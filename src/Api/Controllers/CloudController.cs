using Application.Cloud;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/cloud"")]
public sealed class CloudController : ControllerBase
{
    private readonly CloudProvisioningService _provisioner;
    private readonly CloudCostTracker _costs;

    public CloudController(
        CloudProvisioningService provisioner,
        CloudCostTracker costs)
    {
        _provisioner = provisioner;
        _costs = costs;
    }

    [HttpPost(""scale-up"")]
    public IActionResult ScaleUp()
    {
        var node = _provisioner.Provision();
        return Ok(node);
    }

    [HttpGet(""nodes"")]
    public IActionResult Nodes()
    {
        return Ok(_provisioner.GetActive());
    }
}
