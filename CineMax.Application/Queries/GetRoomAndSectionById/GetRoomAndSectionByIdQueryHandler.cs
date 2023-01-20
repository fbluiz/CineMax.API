using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetRoomAndSectionById
{
    public class GetRoomAndSectionByIdQueryHandler : IRequestHandler<GetRoomAndSectionByIdQuery, RoomAndSectionsViewModel>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomAndSectionByIdQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomAndSectionsViewModel> Handle(GetRoomAndSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var roomAndSections = await _roomRepository.GetByIdRoomAndSectionsAsync(request.Id);

            if (roomAndSections == null)
                return null;

            var roomAndSectionsDetailsViewModel = new RoomAndSectionsViewModel
            {
                Name = roomAndSections.Name,
                IsRoomOcuped = roomAndSections.IsRoomOcuped,
                MaximumCapacity = roomAndSections.Seats.Count,
                DetailsSections = roomAndSections.Sections.Select(s => new DetailsSectionViewModel
                {
                    Name = s.Name,
                    Description = s.Description,
                    MovieName = s.Movie.Title,
                    EndSection = s.EndSection,
                    StartSection = s.StartSection
                }).ToList()
            };
            return roomAndSectionsDetailsViewModel;
        }
    }
}

