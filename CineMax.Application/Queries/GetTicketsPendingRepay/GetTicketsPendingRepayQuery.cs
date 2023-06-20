using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetTicketsPendingRepay
{
    public class GetTicketsPendingRepayQuery : IRequest<List<TicketsPendingRepayViewModel>>
    {
    }
}
