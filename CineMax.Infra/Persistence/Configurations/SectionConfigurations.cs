using CineMax.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CineMax.Infra.Persistence.Configurations
{
    public class SectionConfigurations : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder
            .HasKey(s => s.Id);

            builder
                .HasOne(s => s.Movie)
                .WithMany(m => m.Sections)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(s => s.Room)
                .WithMany(r => r.Sections)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Section)
                .HasForeignKey(t => t.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Status).HasConversion<string>();

        }
    }
}
