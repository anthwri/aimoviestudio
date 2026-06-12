using Application.CinematicUniverse;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/universe"")]
public sealed class UniverseController : ControllerBase
{
    private readonly CinematicUniverseService _service = new();

    [HttpPost(""character"")]
    public IActionResult CreateCharacter(string name, string prompt)
    {
        var character = _service.RegisterCharacter(name, prompt);
        return Ok(character);
    }
}
