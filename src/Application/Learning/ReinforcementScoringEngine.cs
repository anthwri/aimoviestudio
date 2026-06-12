namespace Application.Learning;

public sealed class ReinforcementScoringEngine
{
    public double ScoreAction(double expectedValue, double actualOutcome)
    {
        // simple reward delta system (placeholder RL)
        return actualOutcome - expectedValue;
    }

    public double UpdateBias(double currentBias, double reward)
    {
        var learningRate = 0.1;
        return currentBias + (reward * learningRate);
    }
}
