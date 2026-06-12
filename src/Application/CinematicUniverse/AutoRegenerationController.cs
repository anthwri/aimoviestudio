using Application.Jobs;
using Domain.CinematicUniverse;

namespace Application.CinematicUniverse;

public sealed class AutoRegenerationController
{
    public RenderJob FixIfNeeded(RenderJob job, bool driftDetected)
    {
        if (!driftDetected)
            return job;

        job.Prompt += "", fix identity drift, restore original character appearance, consistent cinematic identity"";

        return job;
    }
}
