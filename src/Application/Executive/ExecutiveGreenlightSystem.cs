using Domain.Executive;

namespace Application.Executive;

public sealed class ExecutiveGreenlightSystem
{
    public bool ShouldGreenlight(FranchiseValue value)
    {
        return value.StrategicPriority > 0.55 &&
               value.NarrativeValue > 0.3 &&
               value.EstimatedCost < 150;
    }

    public string DecisionSummary(FranchiseValue value)
    {
        if (ShouldGreenlight(value))
            return ""GREENLIGHT: Proceed with production"";

        if (value.NarrativeValue < 0.2)
            return ""REJECT: Weak story value"";

        if (value.EstimatedCost > 150)
            return ""REJECT: Too expensive for expected return"";

        return ""HOLD: Needs revision"";
    }
}
