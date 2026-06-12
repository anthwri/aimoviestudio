using Domain.Movies;
using Domain.Continuity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options) { }

    public DbSet<Film> Films => Set<Film>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Scene> Scenes => Set<Scene>();
    public DbSet<Shot> Shots => Set<Shot>();
    public DbSet<ContinuityRecord> ContinuityRecords => Set<ContinuityRecord>();
    public DbSet<FilmExecutionRun> FilmRuns => Set<FilmExecutionRun>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>()
            .HasMany(f => f.Movies);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Scenes);

        modelBuilder.Entity<Scene>()
            .HasMany(s => s.Shots);

        base.OnModelCreating(modelBuilder);
    }
}
