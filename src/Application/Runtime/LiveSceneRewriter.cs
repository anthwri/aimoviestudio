namespace Application.Runtime;

public sealed class LiveSceneRewriter
{
    public string Rewrite(string scenePrompt, string feedbackSignal)
    {
        if (feedbackSignal.Contains("pacing too slow"))
            scenePrompt += ", faster cuts, dynamic motion";

        if (feedbackSignal.Contains("confusing plot"))
            scenePrompt += ", clearer narrative structure";

        if (feedbackSignal.Contains("visual inconsistency"))
            scenePrompt += ", strict continuity enforcement, stable character identity";

        return scenePrompt;
    }
}
