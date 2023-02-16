using CineMax.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineMax.Infra.Persistence.Configurations
{
    public class SeatConfigurations : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder
            .HasKey(s => s.Id);

            builder
                .HasOne(s => s.Room)
                .WithMany(r => r.Seats)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.Status).HasConversion<string>();

        }

    }
}
