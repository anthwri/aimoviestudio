using Domain.Franchise;

namespace Application.Franchise;

public sealed class CanonValidationEngine
{
    public bool Validate(string proposedEvent, List<CanonEvent> canon)
    {
        // simplistic contradiction detection (placeholder for LLM reasoning)

        foreach (var e in canon)
        {
            if (proposedEvent.Contains(""alive"") && e.Description.Contains(""died""))
                return false;

            if (proposedEvent.Contains(""same location"") && e.Description.Contains(""different location""))
                return false;
        }

        return true;
    }
}
