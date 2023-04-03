using MediatR;

namespace CineMax.Application.Commands.DeleteMovieCommand
{
    public class DeleteMovieCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
