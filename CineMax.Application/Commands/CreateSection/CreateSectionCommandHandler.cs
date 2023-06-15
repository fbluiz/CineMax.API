using CineMax.Application.ViewModels;
using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.CreateSection
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, bool>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ISectionSeatRepository _sectionSeatRepository;

        public CreateSectionCommandHandler(ISectionRepository sectionRepository, IRoomRepository roomRepository, ISectionSeatRepository sectionSeatRepository)
        {
            _sectionRepository = sectionRepository;
            _roomRepository = roomRepository;
            _sectionSeatRepository = sectionSeatRepository;
        }

        public async Task<bool> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            bool success = true;
            var room = await _roomRepository.GetRoomAndSeatsByIdAsync(request.RoomId);

            if (request.TicketsDisponible > room.Seats.Count())
                return !success;

            var section = new Section(request.Name, request.Description, request.StartSection, request.EndSection, request.MovieId, request.RoomId, request.TicketsDisponible);

            await _sectionRepository.AddAsync(section);
           
            List<int> seatsDisponible = new List<int>();

            foreach (var seat in room.Seats)
            {
                seatsDisponible.Add(seat.Id);
                var sectionSeat = new SectionSeat(seat.Id, section.Id, true);
                await _sectionSeatRepository.AddAsync(sectionSeat);
            }
            
            return success;
        }
    }
}
