using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineMax.Infra.Persistence.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ICineMaxDbContext _dbContext;

        public MovieRepository(ICineMaxDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _dbContext.Movies.AsNoTracking().ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _dbContext.Movies.Include(m => m.Sections).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
