using Domain.Movies;

namespace Application.Characters;

public sealed class LoraJobBuilder
{
    public LoraTrainingJob Build(CharacterIdentity character, string datasetPath)
    {
        return new LoraTrainingJob
        {
            CharacterName = character.Name,
            DatasetPath = datasetPath,
            Epochs = 12,
            BaseModel = ""sdxl-base-1.0"",
            OutputPath = $""models/lora/{character.Name}.safetensors""
        };
    }
}
