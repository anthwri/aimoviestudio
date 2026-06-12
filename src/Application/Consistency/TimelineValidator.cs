using Domain.Consistency;

namespace Application.Consistency;

public sealed class TimelineValidator
{
    public bool Validate(
        IEnumerable<CanonEvent> events)
    {
        var ordered =
            events.OrderBy(x => x.TimelineOrder)
                  .ToList();

        return ordered.Count == events.Count();
    }
}
