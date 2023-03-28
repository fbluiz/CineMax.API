using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetSectionById
{
    public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, SectionViewModel>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetSectionByIdQueryHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<SectionViewModel> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(s => s.Id == request.Id);

            if (section == null)
                return null;

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
                NameRoom = section.Room.Name,
                StartSection= section.StartSection,
                Status = section.Status.ToString(),
                Tickets = ticketsViewModel
            };

            return SectionViewModel;
        }
    }
}
