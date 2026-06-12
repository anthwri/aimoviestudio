using Application.Orchestration;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/render"")]
public sealed class RenderController : ControllerBase
{
    private readonly FilmRenderPipeline _pipeline;

    public RenderController(FilmRenderPipeline pipeline)
    {
        _pipeline = pipeline;
    }

    [HttpPost(""film"")]
    public IActionResult RenderFilm()
    {
        // placeholder film (in real system comes from DB)
        var film = new Domain.Movies.Film();

        _pipeline.RenderFilm(film);

        return Ok(new
        {
            message = ""Film rendering started"",
            workers = 3
        });
    }
}
