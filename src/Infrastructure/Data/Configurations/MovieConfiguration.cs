using Rihal.ReelRise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rihal.ReelRise.Infrastructure.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        //builder.Property(t => t.Title)
        //    .HasMaxLength(200)
        //    .IsRequired();

        //builder
        //    .OwnsOne(b => b.Colour);
    }
}
