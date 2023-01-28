using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineMax.Infra.Persistence.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ICineMaxDbContext dbContext) : base(dbContext)
        {

        }
    }
}
