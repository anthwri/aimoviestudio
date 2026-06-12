using Application.Runtime;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/cinema"")]
public sealed class CinemaController : ControllerBase
{
    private readonly CinemaRuntimeEngine _runtime;

    public CinemaController(CinemaRuntimeEngine runtime)
    {
        _runtime = runtime;
    }

    [HttpPost(""run"")]
    public async Task<IActionResult> Run(string name, string prompt)
    {
        var stream = await _runtime.RunLiveFilm(name, prompt);
        return Ok(new { stream });
    }
}
