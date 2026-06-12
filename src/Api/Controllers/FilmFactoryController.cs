using Application.Timeline;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/factory"")]
public sealed class FilmFactoryController : ControllerBase
{
    private readonly SceneProductionScheduler _scheduler;
    private readonly VideoAssemblyEngine _assembler;

    public FilmFactoryController(
        SceneProductionScheduler scheduler,
        VideoAssemblyEngine assembler)
    {
        _scheduler = scheduler;
        _assembler = assembler;
    }

    [HttpPost(""scene"")]
    public async Task<IActionResult> ProduceScene(string name, string prompt)
    {
        var scene = await _scheduler.ProduceSceneAsync(name, prompt);
        return Ok(scene);
    }

    [HttpPost(""assemble"")]
    public IActionResult Assemble(SceneSequence scene)
    {
        var video = _assembler.Assemble(scene);
        return Ok(video);
    }
}
