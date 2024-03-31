using Rihal.ReelRise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Rihal.ReelRise.Infrastructure.Data.Configurations;

public class MovieFilmCrewConfiguration : IEntityTypeConfiguration<MovieFilmCrew>
{
    public void Configure(EntityTypeBuilder<MovieFilmCrew> builder)
    {
        builder.HasKey(MF => new { MF.MovieId, MF.FilmCrewId, MF.CrewType });
    }
}
