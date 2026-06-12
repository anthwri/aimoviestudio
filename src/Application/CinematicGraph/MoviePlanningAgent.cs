using Infrastructure.LLM;

namespace Application.CinematicGraph;

public sealed class MoviePlanningAgent
{
    private readonly ILLMClient _llm;

    public MoviePlanningAgent(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> PlanMovie(string idea)
    {
        var system = ""You are a senior film planner. Break ideas into acts, scenes, and visual beats."";

        return await _llm.GenerateAsync(system, idea);
    }
}
