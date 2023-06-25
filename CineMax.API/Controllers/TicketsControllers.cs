using CineMax.Application.Commands.BuyTicket;
using CineMax.Application.Commands.ConfirmRefoundTicket;
using CineMax.Application.Commands.RepayTicket;
using CineMax.Application.Queries.GetMyTickets;
using CineMax.Application.Queries.GetMyTicketsPendingRepay;
using CineMax.Application.Queries.GetTicketsPendingRepay;
using CineMax.Infra.Auth.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CineMax.API.Controllers
{
    [Route("api/tickets")]
    [Authorize]
    public class TicketsControllers : CineMaxBaseController
    {
        private readonly IMediator _mediator;

        public TicketsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = Roles.ClientBasic)]
        public async Task<IActionResult> MyTickets(GetMyTicketsQuery query)
        {
            Guid userId = ExtractUserIdFromToken();
            query.UserId = userId;

            var tickets = await _mediator.Send(query);

            if(tickets == null)
              return NotFound("Tickets not found in database for that customer.");

            return Ok(tickets);
        }

        [HttpPost("buy/{sectionId}")]
        [Authorize(Roles = Roles.ClientBasic)]
        public async Task<IActionResult> BuyTicket([FromRoute] int sectionId,[FromBody] BuyTicketCommand command)
        {
            Guid userId = ExtractUserIdFromToken();
            command.UserId = userId;

            command.SectionId = sectionId;

            var response = await _mediator.Send(command);

            if(response.Errors.Any())
                return BadRequest(response);

            return Ok(response);    
        }

        [HttpPut("/repay/{ticketId}")]
        [Authorize(Roles = Roles.ClientBasic)]
        public async Task<IActionResult> RequestRefundTicket([FromRoute] int ticketId, [FromBody] RepayTicketCommand command)
        {
            Guid userId = ExtractUserIdFromToken();
            command.UserId = userId;
            command.TicketId = ticketId;
            
            var response = await _mediator.Send(command);

            if (response.TicketBelongstoCustomer == false)
                return Forbid("The ticket does not belong to the logged in user.");

            if (response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("/pending-repay")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetTicketsPendingRepay(GetTicketsPendingRepayQuery query)
        {
            var TicketsPendingRepay = await _mediator.Send(query);

            if (TicketsPendingRepay is null)
                return BadRequest("Error fetching pending refund tickets.");

            return Ok(TicketsPendingRepay);
        }

        [HttpGet("/mypending-repay")]
        [Authorize(Roles = Roles.ClientBasic)]
        public async Task<IActionResult> GetMyTicketsPendingRepay(GetMyTicketsPendingRepayQuery query)
        {
            Guid userId = ExtractUserIdFromToken();
            query.UserId = userId;

            var TicketsPendingRepay = await _mediator.Send(query);

            if (TicketsPendingRepay is null)
                return BadRequest("Error fetching pending refund tickets.");

            return Ok(TicketsPendingRepay);
        }

        [HttpPut("/confirmrepay/{ticketId})")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ConfirmRefundTicket([FromRoute] int ticketId, ConfirmRefoundTicketCommand command)
        {
            command.TicketId = ticketId;
            var logResult = await _mediator.Send(command);

            if (logResult is null)
                return NotFound("Refund request not found.");

            return Ok(logResult);
        }
    }
}