using CineMax.Core.DTOs.PaymentDTOs.Request;
using CineMax.Core.Enums;
using CineMax.Core.Logs;
using CineMax.Core.Repositories;
using CineMax.Core.Services.Payment;
using MediatR;

namespace CineMax.Application.Commands.ConfirmRefoundTicket
{
    public class ConfirmRefoundTicketCommandHandler : IRequestHandler<ConfirmRefoundTicketCommand, string>
    {
        public ITicketRepository _ticketRepository { get; set; }
        public IPaymentRefundLogRepository _paymentRefundLogRepository { get; set; }
        public ISectionRepository _sectionRepository { get; set; }
        public IPaymentService _paymentService { get; set; }

        public ConfirmRefoundTicketCommandHandler(ITicketRepository ticketRepository, IPaymentRefundLogRepository paymentRefundLogRepository, ISectionRepository sectionRepository, IPaymentService paymentService)
        {
            _ticketRepository = ticketRepository;
            _paymentRefundLogRepository = paymentRefundLogRepository;
            _sectionRepository = sectionRepository;
            _paymentService = paymentService;
        }

        public async Task<string> Handle(ConfirmRefoundTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(t => t.Id == request.TicketId && t.Status == TicketStatusEnum.RefundRequest);
            PaymentRefundLog log = new PaymentRefundLog(ticket.Id, ticket.ClientId, "");


            if (ticket == null)
                return null;

            if (request.ToApprove)
            {
                var paymentResponse = _paymentService.ApproveRefund(new RefundRequest { BuyIdentity = request.TicketId });

                if (paymentResponse.Success)
                {
                    ticket.Repay();
                    await _ticketRepository.UpdateAsync(ticket);

                    var section = await _sectionRepository.GetByIdAsync(s => s.Id == ticket.SectionId);

                    if (section.Status == SectionStatusEnum.Created)
                    {
                        section.AddTicketDisponible();
                        await _sectionRepository.UpdateSectionAsync(section);
                    }

                    log.LogMessage = "ticket refunded successfully";
                    await _paymentRefundLogRepository.AddAsync(log);

                    return log.LogMessage;
                }
            }

            ticket.RefuseRefund();
            await _ticketRepository.UpdateAsync(ticket);

            log.LogMessage = "Refund refused!";
            await _paymentRefundLogRepository.AddAsync(log);

            return log.LogMessage;
        }
    }
}