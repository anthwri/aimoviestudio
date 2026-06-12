using Domain.Movies;

namespace Application.Characters;

public interface ILoraTrainingRunner
{
    Task<LoraTrainingResult> RunAsync(LoraTrainingJob job, CancellationToken ct);
}
