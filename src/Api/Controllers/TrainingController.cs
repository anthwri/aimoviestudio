using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/training")]
public sealed class TrainingController : ControllerBase
{
    [HttpPost("lora")]
    public IActionResult Train()
    {
        return Ok(new
        {
            message = "LoRA training job created",
            note = "Hook this into Kohya or OneTrainer next phase"
        });
    }
}
