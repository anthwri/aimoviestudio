using Application.StudioCore;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/studio-core"")]
public sealed class StudioCoreController : ControllerBase
{
    private readonly AutonomousStudioOrganism _studio;
    private readonly InternalDebateEngine _debate;

    public StudioCoreController(
        AutonomousStudioOrganism studio,
        InternalDebateEngine debate)
    {
        _studio = studio;
        _debate = debate;
    }

    [HttpPost(""think"")]
    public async Task<IActionResult> Think([FromBody] string trigger)
    {
        var result = await _studio.ThinkAsync(trigger);
        return Ok(result);
    }

    [HttpPost(""debate"")]
    public async Task<IActionResult> Debate([FromBody] string context)
    {
        var result = await _debate.RunDebate(context);
        return Ok(result);
    }
}
