using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Unit>
    {
        private readonly IMovieRepository _movieRepository;

        public UpdateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
          var movie = await _movieRepository.GetByIdAsync(m => m.Id == request.Id);
          var duration = TimeSpan.Parse(request.Duration);

            
          movie.Update(
                request.Title,
                request.Description,
                request.ImageURL,
                request.TrailerURL,
                duration,
                request.Status
                );
            await _movieRepository.UpdateAsync(movie);
          return Unit.Value;
        }
    }
}
