﻿using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineMax.Infra.Persistence.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ICineMaxDbContext dbContext) : base(dbContext)
        {
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
            return await _dbContext.Rooms.Where(r => r.Id ==id && (r.Removed == false || r.Removed == null)).Include(r => r.Seats).FirstOrDefaultAsync();
        }
    }
}
