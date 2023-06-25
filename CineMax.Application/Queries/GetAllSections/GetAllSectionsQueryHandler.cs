using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetAllSections
{
    public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, List<GetSectionViewModel>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRoomRepository _roomRepository;

        public GetAllSectionsQueryHandler(ISectionRepository sectionRepository, IRoomRepository roomRepository)
        {
            _sectionRepository = sectionRepository;
            _roomRepository = roomRepository;
        }

        public async Task<List<GetSectionViewModel>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            var sections = await _sectionRepository.GetSectionsViewModelAsync(request.disponible);

            List<GetSectionViewModel> sectionsViewModel = new List<GetSectionViewModel>();

            foreach (var section in sections)
            {
                var sectionSeat = await _roomRepository.GetSeatsStatusBySection(section.Id);

                var seatsViewModel = sectionSeat.Select(s => new SeatViewModel
                {
                    Position = s.Position,
                    Status = s.IsDisponible ? "Disponible" : "Ocuped",
                    Id = s.Id
                }).ToList();

                GetSectionViewModel sectionViewModel = new GetSectionViewModel
                {
                    SectionId = section.Id,
                    CreatedOn = section.CreatedOn,
                    Description = section.Description,
                    EndSection = section.EndSection,
                    Name = section.Name,
                    NameRoom = section.Room.Name,
                    StartSection = section.StartSection,
                    Status = section.Status.ToString(),
                    TicketDisponibles = section.TicketsDisponible,
                    NameMovie = section.Movie.Title,
                    Seats = seatsViewModel  
                };
                sectionsViewModel.Add(sectionViewModel);
            }

            return sectionsViewModel;
        }
    }
}
