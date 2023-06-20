using CineMax.Application.ViewModels;
using CineMax.Core.Logs;
using MediatR;

namespace CineMax.Application.Commands.RepayTicket
{
    public class RepayTicketCommand : IRequest<RepayTicketLogViewModel>
    {
        public int TicketId { get; set; }
        public Guid UserId { get; set; }
    }
}
