using Application.MemoryFusion;

namespace Application.Prompting;

public sealed class MemoryDrivenPromptBuilder
{
    private readonly MemoryResolver _resolver;

    public MemoryDrivenPromptBuilder(
        MemoryResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task<string> BuildAsync(
        string characterId,
        string environmentId,
        string scenePrompt)
    {
        var memory =
            await _resolver.ResolveCharacterContext(
                characterId,
                environmentId);

        return $"""
CINEMATIC MEMORY CONTEXT

{memory}

SCENE REQUEST

{scenePrompt}

RULE:
Use memory as canonical truth.
Do not contradict visual or narrative identity.
""";
    }
}
