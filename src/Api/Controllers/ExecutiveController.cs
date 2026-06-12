using Application.Executive;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/executive"")]
public sealed class ExecutiveController : ControllerBase
{
    private readonly AudienceSimulator _audience = new();
    private readonly FranchiseStrategyEngine _strategy = new();
    private readonly ExecutiveGreenlightSystem _greenlight = new();

    [HttpPost(""evaluate"")]
    public IActionResult Evaluate(Guid filmId, string script, double cost)
    {
        var engagement = _audience.SimulateEngagement(script);
        var confusion = _audience.SimulateConfusionRisk(script);
        var viral = _audience.SimulateViralPotential(script);

        var value = _strategy.Evaluate(filmId, cost, engagement, viral, confusion);

        return Ok(new
        {
            engagement,
            confusion,
            viral,
            value,
            decision = _greenlight.DecisionSummary(value)
        });
    }
}
