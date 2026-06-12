using Infrastructure.LLM;

namespace Application.Evaluation;

public sealed class StoryEvaluationEngine
{
    private readonly ILLMClient _llm;

    public StoryEvaluationEngine(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> EvaluateAsync(string screenplay)
    {
        return await _llm.GenerateAsync(
            "You are a film critic and story analyst.",
            screenplay);
    }
}
