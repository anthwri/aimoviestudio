using Application.Interfaces.Persistence;
using Domain.Movies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _db;

    public MovieRepository(MovieDbContext db)
    {
        _db = db;
    }

    public async Task<Movie> AddAsync(Movie movie, CancellationToken ct)
    {
        _db.Movies.Add(movie);
        await _db.SaveChangesAsync(ct);
        return movie;
    }

    public async Task<Movie?> GetAsync(Guid id, CancellationToken ct)
    {
        return await _db.Movies
            .Include(m => m.Characters)
            .Include(m => m.Locations)
            .Include(m => m.Props)
            .Include(m => m.Scenes)
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }
}
