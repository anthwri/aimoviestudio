using Domain.Production;

namespace Application.Production;

public sealed class FilmArbitrationEngine
{
    public Guid? SelectNextFilm(List<ProductionJobProfile> films)
    {
        return films
            .OrderByDescending(f => (int)f.Priority)
            .ThenBy(f => f.Spent / (f.BudgetLimit + 0.01m)) // budget pressure
            .ThenBy(f => f.Deadline ?? DateTime.MaxValue)
            .Select(f => f.FilmId)
            .FirstOrDefault();
    }
}
