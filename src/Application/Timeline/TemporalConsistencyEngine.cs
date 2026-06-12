namespace Application.Timeline;

public sealed class TemporalConsistencyEngine
{
    public string Enforce(string basePrompt, int frameIndex)
    {
        var temporalHint =
            frameIndex switch
            {
                < 10 => "establishing shot, stable lighting",
                < 30 => "slight motion, consistent environment",
                _ => "continuous motion, temporal consistency locked"
            };

        return $"{basePrompt}, {temporalHint}, same character identity across frames";
    }
}
