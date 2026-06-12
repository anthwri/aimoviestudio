using System.Text.Json;

namespace Agents.Continuity;

public sealed class ContinuityAgent
{
    public string Validate(string movieJson)
    {
        var prompt = $@"
You are a film continuity supervisor.

Check this movie for continuity issues:

{movieJson}

Rules:
- Characters cannot teleport
- Props cannot appear/disappear
- Clothing must remain consistent unless stated
- Time of day must be consistent
- Location must remain stable per scene

Return ONLY JSON:

{{
  ""issues"": [
    {{
      ""type"": ""error"",
      ""message"": """"
    }}
  ]
}}
";

        return prompt;
    }
}
