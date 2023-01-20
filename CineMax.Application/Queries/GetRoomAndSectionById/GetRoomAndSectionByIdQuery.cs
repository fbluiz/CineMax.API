using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetRoomAndSectionById
{
    public class GetRoomAndSectionByIdQuery : IRequest<RoomAndSectionsViewModel>
    {
        public GetRoomAndSectionByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
