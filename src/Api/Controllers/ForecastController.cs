using Application.Prediction;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/forecast"")]
public sealed class ForecastController : ControllerBase
{
    private readonly FilmForecastEngine _engine;
    private readonly ProductionGreenlightService _greenlight;

    public ForecastController(
        FilmForecastEngine engine,
        ProductionGreenlightService greenlight)
    {
        _engine = engine;
        _greenlight = greenlight;
    }

    [HttpGet(""film"")]
    public IActionResult Forecast()
    {
        var film = new Domain.Movies.Film(); // placeholder

        var forecast = _engine.Forecast(film);
        var decision = _greenlight.Evaluate(film);

        return Ok(new
        {
            forecast,
            decision.Approved,
            decision.Forecast
        });
    }
}
