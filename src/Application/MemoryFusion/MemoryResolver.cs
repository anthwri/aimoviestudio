namespace Application.MemoryFusion;

public sealed class MemoryResolver
{
    private readonly ICinematicMemoryFusionEngine _fusion;

    public MemoryResolver(ICinematicMemoryFusionEngine fusion)
    {
        _fusion = fusion;
    }

    public async Task<string> ResolveCharacterContext(
        string characterId,
        string? environmentId = null)
    {
        var result =
            await _fusion.RetrieveAsync(
                new Domain.MemoryFusion.MemoryFusionRequest
                {
                    CharacterId = characterId,
                    EnvironmentId = environmentId ?? "",
                    Query = "character_context"
                });

        return $"""
CHARACTER MEMORY

Narrative:
{result.NarrativeSummary}

Graph:
{result.GraphContext}

Visual:
{result.VisualContext}

Confidence:
{result.ConfidenceScore}
""";
    }
}
