using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetAllSections
{
    public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, List<SectionViewModel>>
    {
        private readonly ISectionRepository _sectionRepository;
        public GetAllSectionsQueryHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<List<SectionViewModel>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            var sections = await _sectionRepository.GetSectionViewModelAsync();

            var sectionsViewModel = sections.Select(s => new SectionViewModel
            {
                CreatedOn = s.CreatedOn,
                Description = s.Description,
                EndSection = s.EndSection,
                Name = s.Name,
                NameRoom = s.Room.Name,
                StartSection = s.StartSection,
                Status = s.Status.ToString(),
                Tickets = s.Tickets.Select(s => new TicketViewModel
                {
                    Number = s.Id,
                    SeatPosition = s.Seat.Position,
                    Status = s.Status.ToString()
                }).ToList(),
            }).ToList();

            return sectionsViewModel;
        }
    }
}
