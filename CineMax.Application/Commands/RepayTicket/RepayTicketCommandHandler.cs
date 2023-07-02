using CineMax.Application.ViewModels;
using CineMax.Core.DTOs.PaymentDTOs.Request;
using CineMax.Core.Enums;
using CineMax.Core.Interfaces;
using CineMax.Core.Logs;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.RepayTicket
{
    public class RepayTicketCommandHandler : IRequestHandler<RepayTicketCommand, RepayTicketLogViewModel>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentRefundLogRepository _paymentRefundLogRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPaymentService _paymentService;

        public RepayTicketCommandHandler(ITicketRepository ticketRepository, IPaymentRefundLogRepository paymentRefundLogRepository, IClientRepository clientRepository, IPaymentService paymentService)
        {
            _ticketRepository = ticketRepository;
            _paymentRefundLogRepository = paymentRefundLogRepository;
            _clientRepository = clientRepository;
            _paymentService = paymentService;
        }

        public async Task<RepayTicketLogViewModel> Handle(RepayTicketCommand request, CancellationToken cancellationToken)
        {
            var response = new RepayTicketLogViewModel();

            var ticket = await _ticketRepository.GetByIdAsync(t => t.Id == request.TicketId);
            var client = await _clientRepository.GetByIdAsync(c => c.UserId == request.UserId);

            if (ticket.ClientId != client.Id)
            {
                response.Errors.Add("The ticket does not belong to the logged in user.");
                response.TicketBelongstoCustomer = false;
                return response;
            }

            if (ticket.Status == TicketStatusEnum.Validated)
            {
                var RefoundPaymentService = _paymentService.RequestRefund(new RefundRequest
                {
                    BuyIdentity = ticket.Id
                });

                if (!RefoundPaymentService.Success)
                {
                    response.Errors.Add("There was an error requesting a refund.");
                    return response;
                }

                ticket.AwaitingRefundRequest();

                await _ticketRepository.UpdateAsync(ticket);

                PaymentRefundLog paymentRefundLog = new PaymentRefundLog(
                     ticket.Id,
                     ticket.ClientId,
                     "Awaiting refund request"
                     );

                await _paymentRefundLogRepository.AddAsync(paymentRefundLog);
                response.Success = true;

                return response;
            }

            else
            {
                response.AddError("the ticket cannot be refunded");
                return response;
            }
        }
    }
}
