using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<Section>> GetSectionViewModelAsync()
        {
            return await _dbContext.Sections.Include(s => s.Tickets).Include(s => s.Room).ToListAsync();
        }
    }
}
