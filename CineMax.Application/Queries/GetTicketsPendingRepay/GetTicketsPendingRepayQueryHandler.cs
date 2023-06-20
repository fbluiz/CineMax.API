using CineMax.Application.ViewModels;
using CineMax.Core.Enums;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetTicketsPendingRepay
{
    public class GetTicketsPendingRepayQueryHandler : IRequestHandler<GetTicketsPendingRepayQuery, List<TicketsPendingRepayViewModel>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketsPendingRepayQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketsPendingRepayViewModel>> Handle(GetTicketsPendingRepayQuery request, CancellationToken cancellationToken)
        {
            var tickets = (await _ticketRepository.GetAsync()).Where(t => t.Status == TicketStatusEnum.RefundRequest).ToList();

            if (tickets is null)
                return null;

            List<TicketsPendingRepayViewModel> listTicketsPendingRepay = new List<TicketsPendingRepayViewModel>();

            foreach (var ticket in tickets)
            {
                TicketsPendingRepayViewModel TicketsPendingRepayViewModel = new TicketsPendingRepayViewModel
                {
                    clientId = ticket.ClientId,
                    ticketId = ticket.Id,
                    sectionId = ticket.SectionId,
                    seatId = ticket.SeatId,
                    Status = ticket.Status.ToString()
                };

                listTicketsPendingRepay.Add(TicketsPendingRepayViewModel);
            }

            return listTicketsPendingRepay;
        }
    }
}
