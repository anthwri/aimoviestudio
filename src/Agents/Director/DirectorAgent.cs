using Application.Interfaces.Llm;
using Contracts.Director;
using System.Text.Json;

namespace Agents.Director;

public sealed class DirectorAgent
{
    private readonly ILlmClient _llm;

    public DirectorAgent(ILlmClient llm)
    {
        _llm = llm;
    }

    public async Task<MovieResponse> GenerateAsync(
        string idea,
        CancellationToken ct = default)
    {
        var prompt = $@"
You are an elite film director AI.

Create a complete movie from this idea:
{idea}

Return ONLY valid JSON in this format:

{{
  \"title\": \"\",
  \"logline\": \"\",
  \"characters\": [
    {{
      \"name\": \"\",
      \"description\": \"\",
      \"personality\": \"\",
      \"wardrobe\": \"\"
    }}
  ],
  \"locations\": [
    {{
      \"name\": \"\",
      \"description\": \"\"
    }}
  ],
  \"props\": [
    {{
      \"name\": \"\",
      \"description\": \"\"
    }}
  ],
  \"scenes\": [
    {{
      \"sceneNumber\": 1,
      \"summary\": \"\"
    }}
  ]
}}

Rules:
- No markdown
- No explanation
- JSON only
";

        var result = await _llm.PromptAsync(prompt, ct);

        return JsonSerializer.Deserialize<MovieResponse>(
            result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
}
