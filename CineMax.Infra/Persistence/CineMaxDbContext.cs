using CineMax.Core.Entities;
using CineMax.Core.Logs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CineMax.Infra.Persistence
{
    public class CineMaxDbContext : DbContext
    {
        public CineMaxDbContext(DbContextOptions<CineMaxDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<SectionSeat> SectionSeat { get; set; }
        public DbSet<PaymentRefundLog> PaymentRefundLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
