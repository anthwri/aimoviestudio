using Domain.Production;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(""api/production"")]
public sealed class ProductionController : ControllerBase
{
    [HttpPost(""start"")]
    public IActionResult StartFilm()
    {
        var profile = new ProductionJobProfile
        {
            FilmId = Guid.NewGuid(),
            Priority = FilmPriority.High,
            BudgetLimit = 50,
            Deadline = DateTime.UtcNow.AddHours(6)
        };

        return Ok(new
        {
            message = ""Production scheduled"",
            profile
        });
    }
}
