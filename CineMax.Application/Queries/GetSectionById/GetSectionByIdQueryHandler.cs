using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetSectionById
{
    public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, SectionViewModel>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRoomRepository _roomRepository;
        public GetSectionByIdQueryHandler(ISectionRepository sectionRepository, IRoomRepository rooRepository)
        {
            _sectionRepository = sectionRepository;
            _roomRepository = rooRepository;
        }

        public async Task<SectionViewModel> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(s => s.Id == request.Id && (s.Removed == false || s.Removed == null));

            if (section == null) 
                return null;

            var roomName = (await _roomRepository.GetByIdAsync(r => r.Id == request.Id && (r.Removed == false || r.Removed == null))).Name;

            var ticketsViewModel = section.Tickets.Select(t => new TicketViewModel
            {
                Number = t.Id,
                SeatPosition = t.Seat.Position,
                Status = t.Status.ToString(),
            }).ToList();


            var SectionViewModel = new SectionViewModel
            {
                CreatedOn = section.CreatedOn,
                Description= section.Description,
                EndSection = section.EndSection,
                MaximumTickets= section.MaximumTickets,
                Name= section.Name,
                NameRoom = roomName,
                StartSection= section.StartSection,
                Status = section.Status.ToString(),
                Tickets = ticketsViewModel
            };

            return SectionViewModel;
        }
    }
}
