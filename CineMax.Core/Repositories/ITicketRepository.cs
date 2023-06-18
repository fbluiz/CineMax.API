using CineMax.Application.ViewModels;
using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<List<TicketViewModel>> GetTicketsByClientIdAsync(int clientId);
    }
}
