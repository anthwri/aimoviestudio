using Application.Learning;
using Domain.Learning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/learning"")]
public sealed class LearningController : ControllerBase
{
    private readonly OutcomeEvaluationPipeline _pipeline =
        new(new StudioPolicyMemory(), new SelfImprovingStudioController());

    [HttpPost(""outcome"")]
    public IActionResult SubmitOutcome(FilmOutcome outcome)
    {
        _pipeline.EvaluateAndLearn(outcome);

        return Ok(new
        {
            status = ""learned"",
            message = ""Studio policy updated from outcome""
        });
    }
}
