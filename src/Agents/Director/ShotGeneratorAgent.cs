using System.Text.Json;
using Contracts.Director;
using Domain.Movies;

namespace Agents.Director;

public sealed class ShotGeneratorAgent
{
    public List<ShotResponse> Generate(Scene scene, Movie movie)
    {
        var prompt = $@"
Break this scene into cinematic shots:

Scene:
{scene.Summary}

Movie context:
{movie.Title}

Return JSON array:

[
  {{
    ""shotNumber"": 1,
    ""type"": ""wide/close-up/etc"",
    ""cameraDescription"": ""..."",
    ""prompt"": ""cinematic image prompt""
  }}
]

Rules:
- Every action must have a shot
- Include establishing shot
- Include reaction shots
- Maintain continuity
";

        // NOTE: LLM call happens later in integration step
        return new List<ShotResponse>();
    }
}
