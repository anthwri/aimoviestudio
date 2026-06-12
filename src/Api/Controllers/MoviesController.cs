using Agents.Director;
using Contracts.Director;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/movies")]
public sealed class MoviesController : ControllerBase
{
    private readonly DirectorAgent _director;

    public MoviesController(DirectorAgent director)
    {
        _director = director;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<MovieResponse>> Generate(
        CreateMovieRequest request,
        CancellationToken ct)
    {
        var movie = await _director.GenerateAsync(request.Idea, ct);
        return Ok(movie);
    }
}
