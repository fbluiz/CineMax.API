using CineMax.Application.ViewModels;
using CineMax.Core.Enums;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetTicketsPendingRepay
{
    public class GetTicketsPendingRepayQueryHandler : IRequestHandler<GetTicketsPendingRepayQuery, List<TicketsPendingRepayViewModel>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentRefundLogRepository _paymentRefundLogRepository;
        public GetTicketsPendingRepayQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketsPendingRepayViewModel>> Handle(GetTicketsPendingRepayQuery request, CancellationToken cancellationToken)
        {
            var tickets = (await _ticketRepository.GetAsync()).Where(t => t.Status == TicketStatusEnum.RefundRequest && t.Removed == false).ToList();

            if (tickets == null)
                return null;

            List<TicketsPendingRepayViewModel> listTicketsPendingRepay = new List<TicketsPendingRepayViewModel>();

            foreach (var ticket in tickets)
            {
                var logHistory = await _paymentRefundLogRepository.GetLogHistoryByTicket(ticket.Id, ticket.ClientId);

                TicketsPendingRepayViewModel TicketsPendingRepayViewModel = new TicketsPendingRepayViewModel
                {
                    clientId = ticket.ClientId,
                    ticketId = ticket.Id,
                    StatusHistory = logHistory,
                    BuyDateTime = ticket.CreatedOn,
                };

                listTicketsPendingRepay.Add(TicketsPendingRepayViewModel);
            }
            return listTicketsPendingRepay;
        }
    }
}