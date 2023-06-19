using CineMax.Application.ViewModels;
using CineMax.Core.Entities;
using MediatR;

namespace CineMax.Application.Commands.RepayTicket
{
    public class RepayTicketCommand : IRequest<PaymentRefundLog>
    {
        public int TicketId { get; set; }   
    }
}
