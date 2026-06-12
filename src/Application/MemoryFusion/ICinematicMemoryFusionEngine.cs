using Domain.MemoryFusion;

namespace Application.MemoryFusion;

public interface ICinematicMemoryFusionEngine
{
    Task<CinematicMemoryPacket> RetrieveAsync(MemoryFusionRequest request);
}
