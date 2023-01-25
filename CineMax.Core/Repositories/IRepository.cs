using System.Linq.Expressions;

namespace CineMax.Core.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
    }
}
