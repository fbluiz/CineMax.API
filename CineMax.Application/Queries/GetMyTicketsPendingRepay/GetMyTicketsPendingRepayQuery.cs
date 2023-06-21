using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetMyTicketsPendingRepay
{
    public class GetMyTicketsPendingRepayQuery : IRequest<List<MyTicketsPendingRepayViewModel>>
    {
        public Guid UserId { get; set; }
    }
}
