using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMax.Application.Queries.GetMovieById
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieViewModel>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieViewModel> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(request.Id);

            if (movie == null)
                return null;

            var movieViewModel = new MovieViewModel { Title = movie.Title,TrailerURL = movie.TrailerURL,Description = movie.Description, Duration = movie.Duration.ToString(), ImageURL = movie.ImageURL, Status = movie.Status};

            return movieViewModel;
            
        }
    }
}
