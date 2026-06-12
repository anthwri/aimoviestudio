using Application.Studio;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/studio")]
public sealed class StudioController : ControllerBase
{
    private readonly StudioStateService _state;
    private readonly FrameAuditService _audit;

    public StudioController(
        StudioStateService state,
        FrameAuditService audit)
    {
        _state = state;
        _audit = audit;
    }

    [HttpGet("state")]
    public IActionResult GetState()
    {
        return Ok(new
        {
            message = "Studio operational",
            auditCount = _audit.GetAll().Count()
        });
    }
}
