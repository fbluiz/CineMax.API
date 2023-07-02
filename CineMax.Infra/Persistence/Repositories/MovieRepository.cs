using CineMax.Core.Entities;
using CineMax.Core.Repositories;


namespace CineMax.Infra.Persistence.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(CineMaxDbContext dbContext) : base(dbContext)
        {
        }
    }
}
