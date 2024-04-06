using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Movie> Movies { get; }

    DbSet<FilmCrew> FilmCrews { get; }

    DbSet<MovieRate> MovieRates { get; }

    DbSet<Memory> Memories { get; }

    DbSet<MemoryPhoto> MemoryPhoto { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
