using Application.Franchise;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/franchise"")]
public sealed class FranchiseController : ControllerBase
{
    private readonly FranchiseMemory _memory = new();

    [HttpPost(""universe"")]
    public IActionResult CreateUniverse(string name)
    {
        var universe = _memory.CreateUniverse(name);
        return Ok(universe);
    }

    [HttpPost(""canon"")]
    public IActionResult AddCanon(Guid universeId, string eventDesc)
    {
        _memory.AddCanonEvent(universeId, eventDesc);
        return Ok();
    }

    [HttpGet(""canon"")]
    public IActionResult GetCanon(Guid universeId)
    {
        return Ok(_memory.GetCanon(universeId));
    }
}
