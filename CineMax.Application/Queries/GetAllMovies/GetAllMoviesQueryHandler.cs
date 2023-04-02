using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetAllMovies
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<MovieViewModel>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<List<MovieViewModel>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = (await _movieRepository.GetAsync()).Where(m => m.Removed == false || m.Removed == null);

            var moviesViewModel = movies.Select(m => new MovieViewModel { Title = m.Title, Description = m.Description, Duration = m.Duration.ToString(), ImageURL = m.ImageURL, Status = m.Status, TrailerURL = m.TrailerURL }).ToList();

            return moviesViewModel;
        }
    }
}
