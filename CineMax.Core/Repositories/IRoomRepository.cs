using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomAndSectionsAsync();
        Task<Room> GetByIdRoomAndSectionsAsync(int id);
        Task SaveChangesAsync();
        Task<int> AddRoomAsync();
        void UpdateRoom(Room room);
    }
}
