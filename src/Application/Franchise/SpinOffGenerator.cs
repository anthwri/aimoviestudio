using Infrastructure.LLM;

namespace Application.Franchise;

public sealed class SpinOffGenerator
{
    private readonly ILLMClient _llm;

    public SpinOffGenerator(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> GenerateSpinOffIdea(string canonSummary)
    {
        var system = ""You are a franchise producer. Generate spin-off movie ideas based on existing canon."";

        return await _llm.GenerateAsync(system, canonSummary);
    }
}
