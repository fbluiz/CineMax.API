using CineMax.Application.ViewModels;
using CineMax.Core.Enums;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Queries.GetMyTicketsPendingRepay
{
    public class GetMyTicketsPendingRepayQueryHandler : IRequestHandler<GetMyTicketsPendingRepayQuery, List<MyTicketsPendingRepayViewModel>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentRefundLogRepository _paymentRefundLogRepository;
        private readonly IClientRepository _clientRepository;

        public GetMyTicketsPendingRepayQueryHandler(ITicketRepository ticketRepository, IPaymentRefundLogRepository paymentRefundLogRepository, IClientRepository clientRepository)
        {
            _ticketRepository = ticketRepository;
            _paymentRefundLogRepository = paymentRefundLogRepository;
            _clientRepository = clientRepository;
        }

        public async Task<List<MyTicketsPendingRepayViewModel>> Handle(GetMyTicketsPendingRepayQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(c => c.UserId == request.UserId);

            var tickets = (await _ticketRepository.GetAsync()).Where(t => t.Status == TicketStatusEnum.RefundRequest && (t.Removed ?? false) == false && t.ClientId == client.Id).ToList();

            if (tickets == null)
                return null;

            List<MyTicketsPendingRepayViewModel> listTicketsPendingRepay = new List<MyTicketsPendingRepayViewModel>();

            foreach (var ticket in tickets)
            {
                var logHistory = await _paymentRefundLogRepository.GetLogHistoryByTicket(ticket.Id, ticket.ClientId);

                MyTicketsPendingRepayViewModel TicketsPendingRepayViewModel = new MyTicketsPendingRepayViewModel
                {
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
