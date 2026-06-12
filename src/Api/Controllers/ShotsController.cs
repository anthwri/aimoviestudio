using Agents.Director;
using Contracts.Director;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/shots")]
public sealed class ShotsController : ControllerBase
{
    private readonly ShotGeneratorAgent _agent;

    public ShotsController(ShotGeneratorAgent agent)
    {
        _agent = agent;
    }

    [HttpPost("generate")]
    public IActionResult Generate(GenerateShotsRequest request)
    {
        // Placeholder for DB integration in next phase
        return Ok(new { message = ""Shot pipeline ready"" });
    }
}
