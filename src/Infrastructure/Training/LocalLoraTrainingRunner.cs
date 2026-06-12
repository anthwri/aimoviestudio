using Application.Characters;
using Domain.Movies;

namespace Infrastructure.Training;

public sealed class LocalLoraTrainingRunner : ILoraTrainingRunner
{
    public async Task<LoraTrainingResult> RunAsync(
        LoraTrainingJob job,
        CancellationToken ct)
    {
        // THIS IS A SIMULATION LAYER
        // Replace with:
        // - Kohya_ss CLI
        // - OneTrainer API
        // - RunPod job submission

        await Task.Delay(3000, ct);

        var result = new LoraTrainingResult
        {
            JobId = job.Id,
            CharacterName = job.CharacterName,
            ModelPath = job.OutputPath,
            Status = TrainingStatus.Completed,
            Logs = "Simulated training complete"
        };

        return result;
    }
}
