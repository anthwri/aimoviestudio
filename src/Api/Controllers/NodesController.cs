using Application.Infrastructure;
using Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/nodes"")]
public sealed class NodesController : ControllerBase
{
    private readonly RenderNodeRegistry _registry;

    public NodesController(RenderNodeRegistry registry)
    {
        _registry = registry;
    }

    [HttpPost(""register"")]
    public IActionResult Register(RenderNode node)
    {
        _registry.Register(node);
        return Ok(new { message = ""Node registered"" });
    }

    [HttpGet(""status"")]
    public IActionResult Status()
    {
        return Ok(_registry.GetHealthyNodes());
    }
}
