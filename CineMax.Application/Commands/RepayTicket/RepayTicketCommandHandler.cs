using CineMax.Application.ViewModels;
using CineMax.Core.Entities;
using CineMax.Core.Enums;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.RepayTicket
{
    public class RepayTicketCommandHandler : IRequestHandler<RepayTicketCommand, PaymentRefundLog>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentRefundLogRepository _paymentRefundLogRepository;

        public RepayTicketCommandHandler(ITicketRepository ticketRepository, IPaymentRefundLogRepository paymentRefundLogRepository)
        {
            _ticketRepository = ticketRepository;
            _paymentRefundLogRepository = paymentRefundLogRepository;
        }

        public async Task<PaymentRefundLog> Handle(RepayTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(t => t.Id == request.TicketId);

            var PaymentRefundLog = new PaymentRefundLog(
                request.TicketId,
                ticket.ClientId,
                "Awaiting refund request"
                );

            await _paymentRefundLogRepository.AddAsync(PaymentRefundLog);

            ticket.AwaitingRefundRequest();

            await _ticketRepository.UpdateAsync(ticket);

            return PaymentRefundLog;
        }
    }
}
