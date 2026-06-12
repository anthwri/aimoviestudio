using Infrastructure.LLM;
using Infrastructure.Persistence;

namespace Application.StudioCore;

public sealed class ContinuousUniverseLoop
{
    private readonly ILLMClient _llm;
    private readonly CinematicGraphStore _store;

    public ContinuousUniverseLoop(ILLMClient llm, CinematicGraphStore store)
    {
        _llm = llm;
        _store = store;
    }

    public async Task<string> TickAsync()
    {
        var films = string.Join("\n", _store.GetFilms().Select(f => f.Title));

        var system =
            "You are an autonomous cinematic universe engine. " +
            "Continuously evolve stories, characters, and franchises.";

        var output = await _llm.GenerateAsync(system, films);

        return output;
    }
}
