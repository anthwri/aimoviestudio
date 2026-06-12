using Domain.Movies;

namespace Application.Interfaces.Persistence;

public interface IFilmRepository
{
    Task<Film> AddAsync(Film film, CancellationToken ct);
    Task<Film?> GetAsync(Guid id, CancellationToken ct);
}
