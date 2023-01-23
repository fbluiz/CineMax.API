using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task SaveChangesAsync();
    }
}
