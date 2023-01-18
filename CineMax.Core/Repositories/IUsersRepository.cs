using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> UpdateAsync(User user);
        Task<User> DeleteAsync(int id);

    }
}
