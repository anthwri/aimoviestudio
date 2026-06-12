namespace Application.Swarm;

public sealed class LivePromptMutationEngine
{
    public string Mutate(string prompt, string signal)
    {
        if (signal.Contains("low detail"))
            prompt += ", ultra high detail, cinematic lighting";

        if (signal.Contains("face drift"))
            prompt += ", consistent facial structure, identity locked character";

        if (signal.Contains("noise"))
            prompt += ", clean render, sharp focus";

        return prompt;
    }
}
