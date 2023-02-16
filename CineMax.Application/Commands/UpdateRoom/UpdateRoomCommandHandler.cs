using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.UpdateRoomCommand
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, int>
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<int> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdRoomAndSectionsAsync(request.Id);

            room.Update(request.Name, request.IsRoomOcuped);

            await _roomRepository.UpdateAsync(room);

            return room.Id;
        }
    }
}