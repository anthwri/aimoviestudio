namespace Application.Timeline;

public sealed class LiveDirectorOverride
{
    public string OverridePrompt(string prompt, string instruction)
    {
        if (instruction.Contains(""darker tone""))
            prompt += "", cinematic shadows, high contrast lighting"";

        if (instruction.Contains(""faster pacing""))
            prompt += "", dynamic motion, rapid scene cuts"";

        if (instruction.Contains(""emotion""))
            prompt += "", expressive character acting, emotional lighting"";

        return prompt;
    }
}
