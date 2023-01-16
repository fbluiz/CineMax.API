using CineMax.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CineMax.Infra.Persistence.Configurations
{
    public class TicketConfigurations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
            .HasKey(s => s.Id);

            builder
                .HasOne(t => t.User)
                .WithMany(u => u.MyTickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Section)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SectionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
