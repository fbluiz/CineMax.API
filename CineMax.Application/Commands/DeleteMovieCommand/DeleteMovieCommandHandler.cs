using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.DeleteMovieCommand
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand,Unit>
    {
        private readonly IMovieRepository _movieRepository;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(m => m.Id == request.Id);

            movie.delete();

            await _movieRepository.DeleteAsync(movie);

            return Unit.Value; 
        }
    }
}
