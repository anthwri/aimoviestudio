using Domain.Movies;

namespace Application.Characters;

public sealed class LoraTrainingOrchestrator
{
    private readonly ILoraTrainingRunner _runner;

    public LoraTrainingOrchestrator(ILoraTrainingRunner runner)
    {
        _runner = runner;
    }

    public async Task<LoraTrainingResult> TrainCharacterAsync(
        LoraTrainingJob job,
        CancellationToken ct)
    {
        // Step 1: mark queued (future DB state)
        job.Status = TrainingStatus.Running;

        // Step 2: run training
        var result = await _runner.RunAsync(job, ct);

        // Step 3: return model result
        return result;
    }
}
