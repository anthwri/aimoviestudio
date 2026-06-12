using Application.Interfaces.Persistence;
using Domain.Movies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class FilmRepository : IFilmRepository
{
    private readonly MovieDbContext _db;

    public FilmRepository(MovieDbContext db)
    {
        _db = db;
    }

    public async Task<Film> AddAsync(Film film, CancellationToken ct)
    {
        _db.Films.Add(film);
        await _db.SaveChangesAsync(ct);
        return film;
    }

    public async Task<Film?> GetAsync(Guid id, CancellationToken ct)
    {
        return await _db.Films
            .Include(f => f.Movies)
                .ThenInclude(m => m.Scenes)
                    .ThenInclude(s => s.Shots)
            .FirstOrDefaultAsync(f => f.Id == id, ct);
    }
}
