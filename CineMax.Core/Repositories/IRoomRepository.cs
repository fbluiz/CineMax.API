using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomAndSectionsAsync();
        Task<Room> GetByIdRoomAndSectionsAsync(int id);
        Task<Room> UpdateRoomAsync(Room room);
        Task<Room> DeleteRoomAsync(int id);
    }
}
