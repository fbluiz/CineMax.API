using MediatR;

namespace CineMax.Application.Commands.ConfirmRefoundTicket
{
    public class ConfirmRefoundTicketCommand : IRequest<string>
    {
        public int TicketId { get; set; }
        public bool ToApprove { get; set; }
    }
}