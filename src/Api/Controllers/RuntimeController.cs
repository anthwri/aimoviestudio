using Application.Media;
using Domain.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/runtime"")]
public sealed class RuntimeController : ControllerBase
{
    private readonly MediaOrchestrator _media;
    private readonly ReRenderEngine _rerender;

    public RuntimeController(MediaOrchestrator media, ReRenderEngine rerender)
    {
        _media = media;
        _rerender = rerender;
    }

    [HttpPost(""render"")]
    public async Task<IActionResult> Render(RenderJobState job)
    {
        var result = await _media.RenderShotAsync(job);

        if (_rerender.ShouldReRender(result))
            result = _rerender.Retry(result);

        return Ok(result);
    }
}
