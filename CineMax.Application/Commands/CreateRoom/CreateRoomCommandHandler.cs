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
            var room = new Room(request.Name,request.QuantitySeats);
            await _roomRepository.AddAsync(room);

            for (int i = 1; i <= request.QuantitySeats; i++)
            {
                var position = "Seat " + i;
                var seat = new Seat(position, room.Id);

                room.Seats.Add(seat);
                await _roomRepository.AddSeatAsync(seat);
            }

            return room.Id;
        }
    }
}
