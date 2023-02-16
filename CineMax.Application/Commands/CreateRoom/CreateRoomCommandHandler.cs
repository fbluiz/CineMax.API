using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, int>
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = new Room(request.Name);
            
            await _roomRepository.AddAsync(room);

            return room.Id;
        }
    }
}
