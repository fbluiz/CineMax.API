using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using MediatR;


namespace CineMax.Application.Commands.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
    {
        private readonly IMovieRepository _movieRepository;

        public CreateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var duration = TimeSpan.Parse(request.Duration);
            var movie = new Movie(request.Title, request.Description, request.ImageURL, request.TrailerURL, duration, request.Status);

            await _movieRepository.AddAsync(movie);

            return movie.Id;
        }
    }
}
