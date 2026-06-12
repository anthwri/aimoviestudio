using Domain.Studio;

namespace Application.Studio;

public sealed class FrameAuditService
{
    private readonly List<FrameAuditLog> _logs = new();

    public void Log(FrameAuditLog log)
    {
        _logs.Add(log);
    }

    public IEnumerable<FrameAuditLog> GetAll()
        => _logs;
}
