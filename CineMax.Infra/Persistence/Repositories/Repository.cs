using CineMax.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CineMax.Infra.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CineMaxDbContext _dbContext;

        public Repository(CineMaxDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Update(entity);
            
            await SaveChangesAsync();
        }

        public async Task<List<T>> GetAsync()
        {
            return await _dbContext.Set<T>().ToListAsync(); 
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }   

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
            await SaveChangesAsync();
        }
    }
}
