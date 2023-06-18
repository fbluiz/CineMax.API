using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetSectionById
{
    public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, GetSectionViewModel>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRoomRepository _roomRepository;
        public GetSectionByIdQueryHandler(ISectionRepository sectionRepository, IRoomRepository rooRepository)
        {
            _sectionRepository = sectionRepository;
            _roomRepository = rooRepository;
        }

        public async Task<GetSectionViewModel> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetSectionViewModelByIdAsync(request.Id);

            if (section == null) 
                return null;

            var sectionSeat = await _roomRepository.GetSeatsStatusBySection(section.Id);

            var seatsViewModel = sectionSeat.Select(s => new SeatViewModel
            {
                Position = s.Position,
                Status = s.IsDisponible ? "Disponible" : "Ocuped",
                Id= s.Id,
            }).ToList();

            GetSectionViewModel sectionViewModel = new GetSectionViewModel
            {
                CreatedOn = section.CreatedOn,
                Description = section.Description,
                EndSection = section.EndSection,
                Name = section.Name,
                NameRoom = section.Room.Name,
                StartSection = section.StartSection,
                Status = section.Status.ToString(),
                TicketDisponibles = section.TicketsDisponible,
                Seats = seatsViewModel,
                NameMovie = section.Movie.Title
            };

            return sectionViewModel;
        }
    }
}
