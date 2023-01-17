using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
    }
}
