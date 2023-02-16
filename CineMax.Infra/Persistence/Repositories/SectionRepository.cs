using CineMax.Core.Entities;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(ICineMaxDbContext dbContext) : base(dbContext)
        {
        }
        public async Task AddNewTicketAsync(int idSection)
        {
            await _dbContext.Tickets.AddAsync(new Ticket(idSection));
            await SaveChangesAsync();
        }
    }
}
