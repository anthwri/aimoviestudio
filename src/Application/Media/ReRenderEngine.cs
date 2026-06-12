using Domain.Rendering;

namespace Application.Media;

public sealed class ReRenderEngine
{
    public bool ShouldReRender(RenderJobState job)
    {
        if (job.State == RenderState.Rejected)
            return true;

        if (job.OutputPath.Contains(""artifact""))
            return true;

        return false;
    }

    public RenderJobState Retry(RenderJobState job)
    {
        job.State = RenderState.Queued;
        job.Prompt += "", fix continuity, improve visual consistency"";
        return job;
    }
}
