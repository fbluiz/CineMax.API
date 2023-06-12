using CineMax.Core.Entities;
using Microsoft.AspNetCore.SignalR;

namespace CineMax.Core.Repositories
{
    public interface ISectionSeatRepository : IRepository<SectionSeat>
    {
        Task<List<int>> GetSeatsDisponibleBySection(Guid sectionId);
    }
}
