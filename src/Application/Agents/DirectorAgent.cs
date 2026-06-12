using Domain.Agents;
using Domain.Movies;

namespace Application.Agents;

public sealed class DirectorAgent
{
    private readonly Dictionary<Guid, StyleMemory> _styleDb = new();

    public DirectorDecision EvaluateShot(
        Guid filmId,
        string prompt,
        string resultQualityHint)
    {
        // simplistic heuristic "brain" (LLM replaces later)

        if (!_styleDb.ContainsKey(filmId))
        {
            _styleDb[filmId] = new StyleMemory
            {
                FilmId = filmId,
                CharacterStyle = "consistent cinematic character design",
                EnvironmentStyle = "cinematic realism",
                LightingRules = "soft volumetric lighting",
                CameraRules = "35mm film lens, shallow depth of field",
                ForbiddenElements = "extra limbs, distorted faces"
            };
        }

        var style = _styleDb[filmId];

        if (resultQualityHint.Contains("blurry"))
        {
            return new DirectorDecision
            {
                FilmId = filmId,
                Action = "REWRITE_SCENE",
                Reason = "Image quality too low",
                ModifiedPrompt = prompt + ", ultra sharp, high detail, cinematic lighting",
                Confidence = 0.8
            };
        }

        return new DirectorDecision
        {
            FilmId = filmId,
            Action = "CONTINUE",
            Reason = "Acceptable output",
            Confidence = 0.6
        };
    }
}
