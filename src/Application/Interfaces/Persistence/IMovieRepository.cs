using Domain.Movies;

namespace Application.Interfaces.Persistence;

public interface IMovieRepository
{
    Task<Movie> AddAsync(Movie movie, CancellationToken ct);
    Task<Movie?> GetAsync(Guid id, CancellationToken ct);
}
