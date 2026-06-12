using Infrastructure.LLM;

namespace Application.Agents;

public sealed class MultiAgentStudioOrchestrator
{
    private readonly ILLMClient _llm;
    private readonly StudioMemory _memory;

    public MultiAgentStudioOrchestrator(ILLMClient llm, StudioMemory memory)
    {
        _llm = llm;
        _memory = memory;
    }

    public async Task<string> RunStudioMeeting(string sceneDescription)
    {
        var context = _memory.GetContext("cinematic_style");

        // DIRECTOR
        var director = await _llm.GenerateAsync(
            "You are a film director. Decide creative intent.",
            sceneDescription + "\nContext:" + context);

        // CINEMATOGRAPHER
        var cinematographer = await _llm.GenerateAsync(
            "You are a cinematographer. Define camera, lighting, composition.",
            director);

        // EDITOR
        var editor = await _llm.GenerateAsync(
            "You are a film editor. Improve pacing, clarity, structure.",
            cinematographer);

        // FINAL SYNTHESIS
        var final = await _llm.GenerateAsync(
            "You are a senior film producer. Combine all inputs into final shot prompt.",
            director + "\n" + cinematographer + "\n" + editor);

        _memory.Store("cinematic_style", final);

        return final;
    }
}
