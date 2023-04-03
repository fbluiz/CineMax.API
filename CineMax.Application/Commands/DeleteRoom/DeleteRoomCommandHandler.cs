using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Unit>
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(r => r.Id == request.Id);

            room.delete();

            await _roomRepository.DeleteAsync(room);

            return Unit.Value;
        }
    }
}
