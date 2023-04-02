using CineMax.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineMax.Infra.Persistence.Configurations
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
            .HasKey(r => r.Id);

            builder
               .HasMany(r => r.Sections)
               .WithOne(s => s.Room)
               .HasForeignKey(s => s.RoomId)
               .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasMany(r => r.Seats)
                .WithOne(s => s.Room)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}