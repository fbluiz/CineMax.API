using CineMax.Core.Entities;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICineMaxDbContext _dbC0ontext;

        public UserRepository(ICineMaxDbContext dbC0ontext)
        {
            _dbC0ontext = dbC0ontext;
        }
        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
