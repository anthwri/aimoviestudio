namespace Domain.Governance;

public sealed class BudgetTracker
{
    public decimal BudgetLimit { get; set; }

    public decimal CurrentSpend { get; set; }

    public bool IsBudgetExceeded()
    {
        return CurrentSpend >= BudgetLimit;
    }
}
