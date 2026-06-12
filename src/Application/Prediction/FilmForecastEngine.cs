using Domain.Prediction;
using Domain.Movies;
using Application.Jobs;

namespace Application.Prediction;

public sealed class FilmForecastEngine
{
    private readonly WorkloadEstimator _estimator;

    public FilmForecastEngine(WorkloadEstimator estimator)
    {
        _estimator = estimator;
    }

    public FilmForecast Forecast(Film film)
    {
        var jobs = new List<RenderJob>();

        foreach (var movie in film.Movies)
        foreach (var scene in movie.Scenes)
        foreach (var shot in scene.Shots)
        {
            jobs.Add(new RenderJob
            {
                FilmId = film.Id,
                SceneId = scene.Id,
                ShotId = shot.Id,
                Prompt = shot.Prompt
            });
        }

        var totalSeconds = jobs.Sum(j => _estimator.EstimateRenderSeconds(j));
        var totalCost = jobs.Sum(j => _estimator.EstimateCost(j));

        var risk = 0.1;

        if (jobs.Count > 50) risk += 0.2;
        if (totalCost > 20) risk += 0.3;

        return new FilmForecast
        {
            FilmId = film.Id,
            EstimatedDuration = TimeSpan.FromSeconds(totalSeconds),
            EstimatedCost = totalCost,
            RiskScore = Math.Min(1, risk),
            Summary = risk switch
            {
                < 0.3 => "Low risk production",
                < 0.6 => "Moderate risk production",
                _ => "High risk production"
            }
        };
    }
}
