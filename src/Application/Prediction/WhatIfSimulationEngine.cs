using Domain.Production;

namespace Application.Prediction;

public sealed class WhatIfSimulationEngine
{
    public ProductionJobProfile Simulate(
        ProductionJobProfile profile,
        Action<ProductionJobProfile> mutation)
    {
        var clone = new ProductionJobProfile
        {
            FilmId = profile.FilmId,
            Priority = profile.Priority,
            BudgetLimit = profile.BudgetLimit,
            Deadline = profile.Deadline,
            Spent = profile.Spent
        };

        mutation(clone);

        return clone;
    }
}
