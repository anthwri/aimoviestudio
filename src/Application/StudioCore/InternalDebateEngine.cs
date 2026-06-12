using Infrastructure.LLM;

namespace Application.StudioCore;

public sealed class InternalDebateEngine
{
    private readonly ILLMClient _llm;

    public InternalDebateEngine(ILLMClient llm)
    {
        _llm = llm;
    }

    public async Task<string> RunDebate(string context)
    {
        var director = await _llm.GenerateAsync(
            ""You are the Director. Argue creative vision."",
            context);

        var executive = await _llm.GenerateAsync(
            ""You are the Studio Executive. Argue cost, risk, franchise value."",
            director);

        var cinematographer = await _llm.GenerateAsync(
            ""You are the Cinematographer. Argue visual feasibility and style."",
            executive);

        var synthesis = await _llm.GenerateAsync(
            ""You are the Studio Brain. Merge all perspectives into one decision."",
            director + ""\n"" + executive + ""\n"" + cinematographer);

        return synthesis;
    }
}
