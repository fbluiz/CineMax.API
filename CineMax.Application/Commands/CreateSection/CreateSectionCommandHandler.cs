using CineMax.Application.ViewModels;
using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.CreateSection
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, SectionViewModel>
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

        public async Task<SectionViewModel> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetRoomAndSeatsByIdAsync(request.RoomId);

            if (request.TicketsDisponible > room.Seats.Count())
                return null;

            var section = new Section(request.Name, request.Description, request.StartSection, request.EndSection, request.MovieId, request.RoomId, request.TicketsDisponible);

            await _sectionRepository.AddAsync(section);
            string seatsDisponible = string.Join(",", room.Seats.Select(seat => seat.Position));

            foreach (var seat in room.Seats)
            {
                var sectionSeat = new SectionSeat(seat.Id, section.Id, true);
                await _sectionSeatRepository.AddAsync(sectionSeat);
            }
            var sectionViewModel = new SectionViewModel {
                CreatedOn = section.CreatedOn, Description = section.Description, EndSection = section.EndSection,
                TicketDisponibles = section.TickestDisponible, Name = section.Name, NameRoom = section.Room.Name, StartSection = section.StartSection,
                Status = section.Status.ToString(),
                SeatsDisponible = seatsDisponible
            };

            return sectionViewModel;
        }
    }
}
