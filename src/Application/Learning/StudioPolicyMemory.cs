namespace Application.Learning;

public sealed class StudioPolicyMemory
{
    private readonly List<FilmOutcome> _history = new();

    public void Record(FilmOutcome outcome)
    {
        _history.Add(outcome);
    }

    public double GetAverageAudienceScore()
    {
        return _history.Count == 0 ? 0 :
            _history.Average(h => h.AudienceScore);
    }

    public double GetAverageRevenue()
    {
        return _history.Count == 0 ? 0 :
            _history.Average(h => h.RevenueScore);
    }

    public double GetSuccessBias()
    {
        return GetAverageAudienceScore() * 0.6 +
               GetAverageRevenue() * 0.4;
    }
}
