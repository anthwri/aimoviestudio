using Domain.Cloud;

namespace Application.Cloud;

public sealed class CloudCostTracker
{
    private readonly Dictionary<Guid, decimal> _filmSpend = new();

    public void StartFilm(Guid filmId)
    {
        _filmSpend[filmId] = 0;
    }

    public void AddCost(Guid filmId, decimal cost)
    {
        if (!_filmSpend.ContainsKey(filmId))
            _filmSpend[filmId] = 0;

        _filmSpend[filmId] += cost;
    }

    public decimal GetCost(Guid filmId)
    {
        return _filmSpend.TryGetValue(filmId, out var cost)
            ? cost
            : 0;
    }

    public bool IsOverBudget(Guid filmId, decimal limit)
    {
        return GetCost(filmId) > limit;
    }
}
