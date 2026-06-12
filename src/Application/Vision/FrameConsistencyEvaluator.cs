using Domain.Vision;

namespace Application.Vision;

public sealed class FrameConsistencyEvaluator
{
    private readonly VisualSimilarityEngine _similarity;

    public FrameConsistencyEvaluator(
        VisualSimilarityEngine similarity)
    {
        _similarity = similarity;
    }

    public ConsistencyScore Evaluate(
        float[] characterRef,
        float[] environmentRef,
        float[] frameCharacter,
        float[] frameEnvironment,
        float[] frameProps)
    {
        var characterScore =
            _similarity.CosineSimilarity(characterRef, frameCharacter);

        var environmentScore =
            _similarity.CosineSimilarity(environmentRef, frameEnvironment);

        var propScore =
            frameProps.Length == 0
                ? 1.0
                : 0.8; // placeholder baseline

        var overall =
            (characterScore * 0.5) +
            (environmentScore * 0.3) +
            (propScore * 0.2);

        return new ConsistencyScore
        {
            CharacterScore = characterScore,
            EnvironmentScore = environmentScore,
            PropScore = propScore,
            OverallScore = overall
        };
    }
}
