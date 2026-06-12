namespace Domain.Execution;

public enum JobStatus
{
    Pending,
    Running,
    Failed,
    Retrying,
    Complete
}
