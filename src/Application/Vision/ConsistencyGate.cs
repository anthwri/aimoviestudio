namespace Application.Vision;

public sealed class ConsistencyGate
{
    public bool Passes(double score)
    {
        const double threshold = 0.85;

        return score >= threshold;
    }
}
