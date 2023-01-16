using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAllAsync();
    }
}
