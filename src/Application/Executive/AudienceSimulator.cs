namespace Application.Executive;

public sealed class AudienceSimulator
{
    public double SimulateEngagement(string script)
    {
        var score = 0.5;

        if (script.Contains("action")) score += 0.2;
        if (script.Contains("love")) score += 0.1;
        if (script.Length > 500) score -= 0.1;
        if (script.Contains("confusing")) score -= 0.3;

        return Math.Clamp(score, 0, 1);
    }

    public double SimulateConfusionRisk(string script)
    {
        var risk = 0.2;

        if (script.Contains("multiverse")) risk += 0.3;
        if (script.Contains("time travel")) risk += 0.2;
        if (script.Length > 800) risk += 0.2;

        return Math.Clamp(risk, 0, 1);
    }

    public double SimulateViralPotential(string script)
    {
        var viral = 0.3;

        if (script.Contains("twist")) viral += 0.3;
        if (script.Contains("reveal")) viral += 0.2;
        if (script.Contains("betrayal")) viral += 0.2;

        return Math.Clamp(viral, 0, 1);
    }
}
