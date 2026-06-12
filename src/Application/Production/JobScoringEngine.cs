using Domain.Jobs;
using Domain.Production;

namespace Application.Production;

public sealed class JobScoringEngine
{
    public double Score(RenderJob job, ProductionJobProfile profile)
    {
        double score = 0;

        // Priority weight
        score += (int)profile.Priority * 100;

        // Deadline urgency
        if (profile.Deadline.HasValue)
        {
            var hoursLeft = (profile.Deadline.Value - DateTime.UtcNow).TotalHours;
            score += Math.Max(0, 100 - hoursLeft);
        }

        // Budget pressure (lower remaining budget = higher urgency)
        var remainingBudget = profile.BudgetLimit - profile.Spent;
        if (remainingBudget < 5)
        {
            score += 50;
        }

        // Shot complexity penalty (long prompts = more expensive)
        score += job.Prompt.Length / 50.0;

        return score;
    }
}
