using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries
{
    public class GetMyTicketsQuery : IRequest<List<TicketViewModel>>
    {
        public Guid UserId { get; set; }
    }
}
