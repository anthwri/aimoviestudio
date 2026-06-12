using Infrastructure.LLM;

namespace Application.StudioCore;

public sealed class ContinuousFilmFactory
{
    private readonly ILLMClient _llm;

    public ContinuousFilmFactory(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> TickProductionLoop(string backlog)
    {
        var system =
            "You are an autonomous film production factory. " +
            "Continuously select, improve, and generate film scenes for production.";

        return await _llm.GenerateAsync(system, backlog);
    }
}
