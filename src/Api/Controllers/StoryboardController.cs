using Application.CinematicGraph;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/storyboard"")]
public sealed class StoryboardController : ControllerBase
{
    private readonly StoryboardEngine _engine;
    private readonly MoviePlanningAgent _planner;

    public StoryboardController(
        StoryboardEngine engine,
        MoviePlanningAgent planner)
    {
        _engine = engine;
        _planner = planner;
    }

    [HttpPost(""plan"")]
    public async Task<IActionResult> Plan([FromBody] string idea)
    {
        var plan = await _planner.PlanMovie(idea);
        return Ok(plan);
    }

    [HttpPost(""generate"")]
    public async Task<IActionResult> Generate([FromBody] string script)
    {
        var storyboard = await _engine.GenerateStoryboard(script);
        return Ok(storyboard);
    }
}
