using CineMax.Core.Entities;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(CineMaxDbContext dbContext) : base(dbContext)
        {
        }
    }
}
