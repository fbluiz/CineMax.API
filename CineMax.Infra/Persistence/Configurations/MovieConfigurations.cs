using CineMax.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CineMax.Infra.Persistence.Configurations
{
    public class MovieConfigurations : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
            .HasKey(m => m.Id);

            builder
                .HasMany(m => m.Sections)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
