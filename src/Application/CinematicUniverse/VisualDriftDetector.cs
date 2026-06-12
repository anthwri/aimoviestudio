namespace Application.CinematicUniverse;

public sealed class VisualDriftDetector
{
    public bool Detect(string expectedIdentity, string actualOutputHint)
    {
        // heuristic placeholder for real embedding similarity
        if (string.IsNullOrEmpty(actualOutputHint))
            return false;

        var mismatchSignals = new[]
        {
            "different face",
            "changed character",
            "inconsistent identity",
            "extra person",
            "wrong outfit"
        };

        return mismatchSignals.Any(actualOutputHint.Contains);
    }
}
