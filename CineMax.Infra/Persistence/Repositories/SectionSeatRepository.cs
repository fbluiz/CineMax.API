using CineMax.Core.Entities;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class SectionSeatRepository : Repository<SectionSeat>, ISectionSeatRepository
    {
        public SectionSeatRepository(CineMaxDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<int>> GetSeatsDisponibleBySection(Guid sectionId)
        {
            throw new NotImplementedException();
        }
    }
}
