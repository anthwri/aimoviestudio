using Domain.Jobs;

namespace Application.Agents;

public sealed class LLMDirectorService
{
    private readonly MultiAgentStudioOrchestrator _studio;

    public LLMDirectorService(MultiAgentStudioOrchestrator studio)
    {
        _studio = studio;
    }

    public async Task<RenderJob> CreateShot(RenderJob job)
    {
        var refined = await _studio.RunStudioMeeting(job.Prompt);

        job.Prompt = refined;

        return job;
    }
}
