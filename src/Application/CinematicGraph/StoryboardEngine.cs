using Infrastructure.LLM;
using Domain.CinematicGraph;

namespace Application.CinematicGraph;

public sealed class StoryboardEngine
{
    private readonly ILLMClient _llm;

    public StoryboardEngine(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<List<ShotNode>> GenerateStoryboard(string script)
    {
        var system = @"You are a film storyboard generator.
Return structured JSON-like shot breakdown with:
- scene description
- camera angle
- visual prompt
- lighting
Keep consistency across shots.";

        var response = await _llm.GenerateAsync(system, script);

        // NOTE: in real system → JSON parsing
        return new List<ShotNode>
        {
            new ShotNode
            {
                Prompt = response,
                CameraAngle = "wide cinematic shot"
            }
        };
    }
}
