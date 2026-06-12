using Domain.Events;

namespace Application.Interfaces.Persistence;

public interface IFilmEventRepository
{
    Task AddAsync(FilmEvent evt, CancellationToken ct);
    Task<List<FilmEvent>> GetByFilmAsync(Guid filmId, CancellationToken ct);
}
