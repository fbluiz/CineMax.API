using CineMax.Core.DTOs.PaymentDTOs.Request;
using CineMax.Core.Enums;
using CineMax.Core.Interfaces;
using CineMax.Core.Logs;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.ConfirmRefoundTicket
{
    public class ConfirmRefoundTicketCommandHandler : IRequestHandler<ConfirmRefoundTicketCommand, string>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentRefundLogRepository _paymentRefundLogRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IPaymentService _paymentService;
        public ConfirmRefoundTicketCommandHandler(ITicketRepository ticketRepository, IPaymentRefundLogRepository paymentRefundLogRepository, ISectionRepository sectionRepository, IPaymentService paymentService, IRoomRepository roomRepository)
        {
            _ticketRepository = ticketRepository;
            _paymentRefundLogRepository = paymentRefundLogRepository;
            _sectionRepository = sectionRepository;
            _paymentService = paymentService;
            _roomRepository = roomRepository;
        }

        public async Task<string> Handle(ConfirmRefoundTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(t => t.Id == request.TicketId && t.Status == TicketStatusEnum.RefundRequest);
            PaymentRefundLog log = new PaymentRefundLog(ticket.Id, ticket.ClientId, "");


            if (ticket == null || ticket.Status != TicketStatusEnum.RefundRequest)
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
                        // Caso a Seção ainda esteja open deve adcionar +1 na quantidade de tickets disponiveis
                        section.AddTicketDisponible();
                        await _sectionRepository.UpdateSectionAsync(section);

                        // Alterar a Cadeira do ticket reembolsado para disponível
                        var seatSectionStatus = await _roomRepository.GetSectionSeatAsync(section.Id, ticket.SeatId);
                        seatSectionStatus.ChangeAvailability(true);
                        await _roomRepository.UpdateSectionSeatAsync(seatSectionStatus);
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