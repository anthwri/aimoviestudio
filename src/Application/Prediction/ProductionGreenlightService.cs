using Domain.Movies;
using Domain.Prediction;

namespace Application.Prediction;

public sealed class ProductionGreenlightService
{
    private readonly FilmForecastEngine _forecast;

    public ProductionGreenlightService(FilmForecastEngine forecast)
    {
        _forecast = forecast;
    }

    public (bool Approved, FilmForecast Forecast) Evaluate(Film film)
    {
        var forecast = _forecast.Forecast(film);

        var approved =
            forecast.RiskScore < 0.7 &&
            forecast.EstimatedCost < 100;

        return (approved, forecast);
    }
}
