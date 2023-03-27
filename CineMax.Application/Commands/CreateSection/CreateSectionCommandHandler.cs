using CineMax.Application.ViewModels;
using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using MediatR;
using System.IO.MemoryMappedFiles;

namespace CineMax.Application.Commands.CreateSection
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, SectionViewModel>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateSectionCommandHandler(ISectionRepository sectionRepository, IRoomRepository roomRepository)
        {
            _sectionRepository = sectionRepository;
            _roomRepository = roomRepository;
        }

        public async Task<SectionViewModel> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetRoomAndSeatsByIdAsync(request.RoomId);

            if (request.MaximumTickets > room.Seats.Count())
                return null;

            var section = new Section(request.Name, request.Description, request.StartSection, request.EndSection, request.MovieId, request.RoomId, request.MaximumTickets);

            await _sectionRepository.AddAsync(section);

            var ticketViewModel = section.Tickets.Select(t => new TicketViewModel { Number = t.Id, SeatPosition = t.Seat.Position, Status = t.Status.ToString() }).ToList(); 

            var sectionViewModel = new SectionViewModel { CreatedOn= section.CreatedOn,Description = section.Description, EndSection = section.EndSection, MaximumTickets = section.MaximumTickets, Name = section.Name, NameRoom = section.Room.Name, StartSection = section.StartSection, Status = section.Status.ToString(), Tickets = ticketViewModel };

            return (sectionViewModel);
        }
    }
}
