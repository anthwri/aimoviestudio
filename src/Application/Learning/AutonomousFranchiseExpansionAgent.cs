using Infrastructure.LLM;

namespace Application.Learning;

public sealed class AutonomousFranchiseExpansionAgent
{
    private readonly ILLMClient _llm;

    public AutonomousFranchiseExpansionAgent(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> GenerateNextFilmIdea(string canon, double successBias)
    {
        var system =
            ""You are an autonomous studio expansion AI. "" +
            ""Generate new film ideas based on what has been successful in the past."";

        var input =
            $""Canon:\n{canon}\nSuccessBias:{successBias}"";

        return await _llm.GenerateAsync(system, input);
    }
}
