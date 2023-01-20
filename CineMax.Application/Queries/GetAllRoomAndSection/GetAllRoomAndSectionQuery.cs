using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetAllRoom
{
    public class GetAllRoomAndSectionQuery : IRequest<List<RoomAndSectionsViewModel>>
    {

    }
}
