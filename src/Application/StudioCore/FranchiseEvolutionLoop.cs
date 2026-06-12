using Infrastructure.LLM;

namespace Application.StudioCore;

public sealed class FranchiseEvolutionLoop
{
    private readonly ILLMClient _llm;

    public FranchiseEvolutionLoop(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> Evolve(string canonState)
    {
        var system =
            ""You are an evolving cinematic universe engine. "" +
            ""Continuously propose new films, arcs, and universe expansions."";

        return await _llm.GenerateAsync(system, canonState);
    }
}
