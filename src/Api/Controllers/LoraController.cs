using Application.Characters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/lora")]
public sealed class LoraController : ControllerBase
{
    private readonly LoraTrainingOrchestrator _orchestrator;

    public LoraController(LoraTrainingOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    [HttpPost("train")]
    public async Task<IActionResult> Train(CancellationToken ct)
    {
        var job = new LoraTrainingJob
        {
            CharacterName = "DemoCharacter",
            DatasetPath = "training_data/DemoCharacter",
            Epochs = 10,
            BaseModel = "sdxl-base-1.0",
            OutputPath = "models/lora/DemoCharacter.safetensors"
        };

        var result = await _orchestrator.TrainCharacterAsync(job, ct);

        return Ok(result);
    }
}
