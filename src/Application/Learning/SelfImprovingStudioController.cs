namespace Application.Learning;

public sealed class SelfImprovingStudioController
{
    private double _creativeBias = 0.5;
    private double _riskTolerance = 0.5;

    public void ApplyOutcome(FilmOutcome outcome)
    {
        var reward =
            (outcome.AudienceScore * 0.5) +
            (outcome.RevenueScore * 0.3) -
            (1 - outcome.CriticalScore);

        _creativeBias += reward * 0.05;
        _riskTolerance += (outcome.AudienceScore - 0.5) * 0.02;

        _creativeBias = Math.Clamp(_creativeBias, 0, 1);
        _riskTolerance = Math.Clamp(_riskTolerance, 0, 1);
    }

    public (double creativity, double risk) GetPolicy()
    {
        return (_creativeBias, _riskTolerance);
    }
}
