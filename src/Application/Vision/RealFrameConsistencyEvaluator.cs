namespace Application.Vision;

public sealed class RealFrameConsistencyEvaluator
{
    private readonly VisualSimilarityEngine _similarity;

    public RealFrameConsistencyEvaluator(
        VisualSimilarityEngine similarity)
    {
        _similarity = similarity;
    }

    public double EvaluateCharacterConsistency(
        float[] reference,
        float[] frame)
    {
        return _similarity.CosineSimilarity(reference, frame);
    }
}
