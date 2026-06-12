namespace Application.Learning;

public sealed class OutcomeEvaluationPipeline
{
    private readonly StudioPolicyMemory _memory;
    private readonly SelfImprovingStudioController _controller;

    public OutcomeEvaluationPipeline(
        StudioPolicyMemory memory,
        SelfImprovingStudioController controller)
    {
        _memory = memory;
        _controller = controller;
    }

    public void EvaluateAndLearn(FilmOutcome outcome)
    {
        _memory.Record(outcome);
        _controller.ApplyOutcome(outcome);
    }
}
