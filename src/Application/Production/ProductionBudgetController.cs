using Domain.Production;

namespace Application.Production;

public sealed class ProductionBudgetController
{
    public bool CanSpend(ProductionJobProfile profile, decimal cost)
    {
        return (profile.Spent + cost) <= profile.BudgetLimit;
    }

    public void AddSpend(ProductionJobProfile profile, decimal cost)
    {
        profile.Spent += cost;
    }

    public bool IsOverBudget(ProductionJobProfile profile)
    {
        return profile.Spent > profile.BudgetLimit;
    }
}
