using CineMax.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CineMax.Infra.Persistence
{
    public class ICineMaxDbContext : DbContext
    {
        //Classe que são criadas as tabelas e passadas por EntityFramework
        //Baixar os pacotes nuget Ef Core tools, Ef Core Design, Ef Core SQL
        public ICineMaxDbContext(DbContextOptions<ICineMaxDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
