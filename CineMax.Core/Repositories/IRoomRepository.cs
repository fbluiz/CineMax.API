using CineMax.Application.ViewModels;
using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<List<Room>> GetAllRoomAndSectionsAsync();
        Task<Room> GetByIdRoomAndSectionsAsync(int id);
        Task AddSeatAsync(Seat seat);
        Task<Room> GetRoomAndSeatsByIdAsync(int id);
        Task<List<GetSeatsStatusBySectionViewModel>> GetSeatsStatusBySection(int sectionId);
    }
}
