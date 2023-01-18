using CineMax.Core.Entities;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICineMaxDbContext _dbContext;

        public UserRepository(ICineMaxDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
