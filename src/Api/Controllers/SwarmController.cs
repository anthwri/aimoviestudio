using Application.Swarm;
using Domain.Swarm;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/swarm"")]
public sealed class SwarmController : ControllerBase
{
    private readonly RenderSwarmDispatcher _dispatcher;

    public SwarmController(RenderSwarmDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost(""render"")]
    public async Task<IActionResult> Render(RenderTask task)
    {
        var result = await _dispatcher.DispatchAsync(task);
        return Ok(result);
    }
}
