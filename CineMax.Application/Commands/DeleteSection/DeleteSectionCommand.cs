using MediatR;

namespace CineMax.Application.Commands.DeleteMovieCommand
{
    public class DeleteSectionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
