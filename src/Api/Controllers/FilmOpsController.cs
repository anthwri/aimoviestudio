using Application.Events;
using Application.Inspection;
using Application.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/film"")]
public sealed class FilmOpsController : ControllerBase
{
    private readonly FilmTimelineService _timeline;
    private readonly ShotInspectorService _inspector;
    private readonly FilmAnalyticsService _analytics;

    public FilmOpsController(
        FilmTimelineService timeline,
        ShotInspectorService inspector,
        FilmAnalyticsService analytics)
    {
        _timeline = timeline;
        _inspector = inspector;
        _analytics = analytics;
    }

    [HttpGet(""{filmId}/timeline"")]
    public async Task<IActionResult> Timeline(Guid filmId)
    {
        return Ok(await _timeline.GetTimeline(filmId));
    }

    [HttpGet(""{filmId}/inspect"")]
    public async Task<IActionResult> Inspect(Guid filmId)
    {
        return Ok(await _inspector.InspectFilm(filmId));
    }

    [HttpGet(""{filmId}/stats"")]
    public async Task<IActionResult> Stats(Guid filmId)
    {
        return Ok(await _analytics.GetStats(filmId));
    }
}
