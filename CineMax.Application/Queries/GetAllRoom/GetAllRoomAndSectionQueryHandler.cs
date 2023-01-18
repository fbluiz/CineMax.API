using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetAllRoom
{
    public class GetAllRoomAndSectionQueryHandler : IRequestHandler<GetAllRoomAndSectionQuery, List<RoomAndSectionsViewModel>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomAndSectionQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomAndSectionsViewModel>> Handle(GetAllRoomAndSectionQuery request, CancellationToken cancellationToken)
        {

            var roomsAndSections = await _roomRepository.GetAllRoomAndSectionsAsync();

            var roomsAndSectionsViewModel = new List<RoomAndSectionsViewModel>();

            foreach(var roomAndSection in roomsAndSections)
            {
                var roomAndSectionViewModel = new RoomAndSectionsViewModel
                {
                    Name = roomAndSection.Name,
                    IsRoomOcuped = roomAndSection.IsRoomOcuped,
                    MaximumCapacity = roomAndSection.Seats.Count,
                    DetailsSections = roomAndSection.Sections.Select(s => new DetailsSectionViewModel
                    {
                        Description = s.Description,
                        EndSection = s.EndSection,
                        MovieName = s.Movie.Title,
                        Name = s.Name,
                        StartSection = s.StartSection
                    }).ToList()
                };
                roomsAndSectionsViewModel.Add(roomAndSectionViewModel);
            }
            return roomsAndSectionsViewModel;
        }
    }
}
