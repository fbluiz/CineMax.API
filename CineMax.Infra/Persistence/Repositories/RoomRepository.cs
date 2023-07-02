using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using CineMax.Application.ViewModels;

namespace CineMax.Infra.Persistence.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly string _connectionString;

        public RoomRepository(CineMaxDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _connectionString = configuration.GetConnectionString("CineMaxCsVini");
        }

        public async Task AddSeatAsync(Seat seat)
        {
            await _dbContext.Seats.AddAsync(seat);
            await SaveChangesAsync();
        }

        public async Task<List<Room>> GetAllRoomAndSectionsAsync()
        {
            return await _dbContext.Rooms.Where(r => r.Removed == false || r.Removed == null).Include(r => r.Sections).ThenInclude(r => r.Movie).Include(r => r.Seats).AsNoTracking().ToListAsync();
        }

        public async Task<Room> GetByIdRoomAndSectionsAsync(int id)
        {
            return await _dbContext.Rooms.Where(r => r.Id == id && (r.Removed == false || r.Removed == null)).Include(r => r.Sections).ThenInclude(r => r.Movie).Include(r => r.Seats).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Room> GetRoomAndSeatsByIdAsync(int id)
        {
            return await _dbContext.Rooms.Where(r => r.Id == id && (r.Removed == false || r.Removed == null)).Include(r => r.Seats).FirstOrDefaultAsync();
        }

        public async Task<Seat> GetSeatByIdAsync(int seatId)
        {
            return await _dbContext.Seats.FirstOrDefaultAsync(s => s.Id == seatId);
        }

        public async Task<List<GetSeatsStatusBySectionViewModel>> GetSeatsStatusBySection(int sectionId, int? seatId = null)
        {
            string whereClause = "";

            if (seatId != null)
            {
                whereClause = "and s.id = @seatId";
            }
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = @$"select s.Position, ss.IsDisponible, s.Id
                            from SectionSeat ss
                            join Seats s on s.Id = ss.SeatId
                            where ss.SectionId = @sectionId
                            {whereClause}";
                            

                return (await sqlConnection.QueryAsync<GetSeatsStatusBySectionViewModel>(script, new { sectionId, seatId })).ToList();
            }
        
        }

        public async Task<SectionSeat> GetSectionSeatAsync(int sectionId, int seatId)
        {
            return await _dbContext.SectionSeat.FirstOrDefaultAsync(ss => ss.SeatId== seatId && ss.SectionId == sectionId);
        }

        public async Task UpdateSectionSeatAsync(SectionSeat sectionSeat)
        {           
           _dbContext.Entry(sectionSeat).State = EntityState.Modified;
           _dbContext.Set<SectionSeat>().Update(sectionSeat);
           await SaveChangesAsync();   
        }
    }
}
