using Infrastructure.LLM;

namespace Application.Runtime;

public sealed class InfiniteCinemaLoop
{
    private readonly ILLMClient _llm;

    public InfiniteCinemaLoop(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> TickAsync(string universeState)
    {
        var system =
            ""You are a continuous cinematic universe generator. "" +
            ""You never stop producing, improving, and evolving films."";

        return await _llm.GenerateAsync(system, universeState);
    }
}
