using CineMax.Core.Entities;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public Task<User> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
