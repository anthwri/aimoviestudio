using Infrastructure.LLM;
using Domain.Learning;

namespace Application.StudioCore;

public sealed class AutonomousStudioOrganism
{
    private readonly ILLMClient _llm;
    private StudioState _state = new();

    public AutonomousStudioOrganism(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> ThinkAsync(string trigger)
    {
        var system =
            "You are an autonomous film studio intelligence. " +
            "You manage universe continuity, creative direction, executive decisions, " +
            "and long-term franchise evolution. Always reason globally.";

        var input =
            $"STUDIO STATE:\n{_state.UniverseMemory}\n{_state.CanonHistory}\n{_state.LearningState}\n" +
            $"TRIGGER:\n{trigger}";

        var output = await _llm.GenerateAsync(system, input);

        _state.LearningState = output;
        _state.LastUpdated = DateTime.UtcNow;

        return output;
    }
}
