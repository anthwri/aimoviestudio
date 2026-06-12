namespace Application.MemoryFusion;

public sealed class MemoryQualityScorer
{
    public double ScoreConsistency(
        double vectorConfidence,
        double graphCompleteness,
        double visualMatch)
    {
        return
            (vectorConfidence * 0.4) +
            (graphCompleteness * 0.3) +
            (visualMatch * 0.3);
    }
}
