using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/characters")]
public sealed class CharactersController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register()
    {
        return Ok(new
        {
            message = ""Character registered (stub)"",
            note = ""LoRA training comes in Phase 2.4""
        });
    }
}
