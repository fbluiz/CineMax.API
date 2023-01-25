using CineMax.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineMax.Infra.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ICineMaxDbContext _dbContext;

        public Repository(ICineMaxDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }
        
        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
        }
    }
}
