using CineMax.Application.ViewModels;
using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<List<Room>> GetAllRoomAndSectionsAsync();
        Task<Room> GetByIdRoomAndSectionsAsync(int id);
        Task AddSeatAsync(Seat seat);
        Task UpdateSectionSeatAsync(SectionSeat sectionSeat);
        Task<Room> GetRoomAndSeatsByIdAsync(int id);
        Task<List<GetSeatsStatusBySectionViewModel>> GetSeatsStatusBySection(int sectionId,int? seatId = null);
        Task<SectionSeat> GetSectionSeatAsync(int sectionId, int seatId);
    }
}
