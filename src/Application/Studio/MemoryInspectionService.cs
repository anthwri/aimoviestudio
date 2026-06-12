using Domain.Studio;
using Application.MemoryFusion;

namespace Application.Studio;

public sealed class MemoryInspectionService
{
    private readonly MemoryResolver _resolver;

    public MemoryInspectionService(MemoryResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task<MemorySnapshot> InspectCharacter(string id)
    {
        var context =
            await _resolver.ResolveCharacterContext(id);

        return new MemorySnapshot
        {
            EntityId = id,
            Type = "character",
            Summary = context,
            Confidence = 0.8
        };
    }
}
