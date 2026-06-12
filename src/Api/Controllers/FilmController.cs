using Application.Orchestration;
using Contracts.Director;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/film"")]
public sealed class FilmController : ControllerBase
{
    private readonly FilmExecutionService _service;

    public FilmController(FilmExecutionService service)
    {
        _service = service;
    }

    [HttpPost(""execute"")]
    public async Task<IActionResult> Execute(
        CreateFilmRequest request,
        CancellationToken ct)
    {
        var film = await _service.ExecuteAsync(request, ct);
        return Ok(film);
    }
}
