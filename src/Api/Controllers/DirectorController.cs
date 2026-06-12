using Application.Agents;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/director"")]
public sealed class DirectorController : ControllerBase
{
    private readonly DirectorAgent _agent;

    public DirectorController(DirectorAgent agent)
    {
        _agent = agent;
    }

    [HttpPost(""evaluate"")]
    public IActionResult Evaluate(Guid filmId, string prompt, string hint)
    {
        var result = _agent.EvaluateShot(filmId, prompt, hint);
        return Ok(result);
    }
}
