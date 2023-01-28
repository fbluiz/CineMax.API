using System.Linq.Expressions;

namespace CineMax.Core.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
    }
}
