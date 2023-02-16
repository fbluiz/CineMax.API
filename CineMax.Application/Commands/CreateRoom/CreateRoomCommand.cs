using MediatR;

namespace CineMax.Application.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
