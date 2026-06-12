using Domain.Vision;

namespace Application.Vision;

public sealed class SceneConsistencyAggregator
{
    public ConsistencyScore Aggregate(
        IEnumerable<ConsistencyScore> scores)
    {
        var list = scores.ToList();

        if (!list.Any())
            return new ConsistencyScore();

        return new ConsistencyScore
        {
            CharacterScore =
                list.Average(x => x.CharacterScore),

            EnvironmentScore =
                list.Average(x => x.EnvironmentScore),

            PropScore =
                list.Average(x => x.PropScore),

            OverallScore =
                list.Average(x => x.OverallScore)
        };
    }
}
