using CineMax.Application.ViewModels;
using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static System.Collections.Specialized.BitVector32;


namespace CineMax.Infra.Persistence.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly string _connectionString;
        public TicketRepository(ICineMaxDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _connectionString = configuration.GetConnectionString("CineMaxCsVini");
        }

        public async Task<List<TicketViewModel>> GetTicketsByClientIdAsync(int clientId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string script = @"select m.Title nameMovie,t.Id,st.Position,t.Status,s.Name,s.Id,r.Name
                                  from Tickets t
                                  join Clients c on c.Id = t.ClientId 
                                  join Sections s on s.Id = t.SectionId
                                  join Rooms r on r.Id = s.RoomId 
                                  join Movies m on m.Id = s.MovieId
                                  join SectionSeat ss on ss.SectionId = s.Id
                                  join Seats st on st.Id = ss.SeatId
                                  where t.ClientId = @ClientId";

                return (await sqlConnection.QueryAsync<TicketViewModel>(script, new { clientId })).ToList();
                 
            }
        }
    }
}
