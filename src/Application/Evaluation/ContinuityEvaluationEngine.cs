namespace Application.Evaluation;

public sealed class ContinuityEvaluationEngine
{
    public double ScoreConsistency(
        string expectedCharacter,
        string actualCharacter)
    {
        if(expectedCharacter == actualCharacter)
            return 1.0;

        return 0.5;
    }
}
