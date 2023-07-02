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
        public TicketRepository(CineMaxDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _connectionString = configuration.GetConnectionString("CineMaxCsVini");
        }

        public async Task<List<TicketViewModel>> GetTicketsByClientIdAsync(int clientId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string script = @"select t.Id TicketId, s.Id SectionId,s.StartSection StartSectionBase , s.EndSection EndSectionBase, s.Name NameSection, m.Title NameMovie, st.Position SeatPosition, t.Status, r.name nameRoom
                                  from Tickets t
                                  join Sections s on t.SectionId = s.Id
                                  join rooms r on r.id = s.RoomId
                                  join Movies m on m.Id = s.MovieId
                                  join Seats st on st.Id = t.SeatId
                                  join SectionSeat ss on ss.SeatId = st.id and ss.SectionId = s.id
                                   where t.ClientId = @ClientId";
                return (await sqlConnection.QueryAsync<TicketViewModel>(script, new { clientId })).ToList();
                 
            }
        }
    }
}
