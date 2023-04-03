using MediatR;

namespace CineMax.Application.Commands.DeleteRoom
{
    public class DeleteRoomCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
