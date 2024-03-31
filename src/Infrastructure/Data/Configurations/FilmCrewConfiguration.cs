using Rihal.ReelRise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rihal.ReelRise.Infrastructure.Data.Configurations;

public class FilmCrewConfiguration : IEntityTypeConfiguration<FilmCrew>
{
    public void Configure(EntityTypeBuilder<FilmCrew> builder)
    {
        //builder.Property(t => t.Title)
        //    .HasMaxLength(200)
        //    .IsRequired();
    }
}
