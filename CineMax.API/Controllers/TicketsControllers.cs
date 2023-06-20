using CineMax.Application.Commands.BuyTicket;
using CineMax.Application.Commands.RepayTicket;
using CineMax.Application.Queries.GetMovieById;
using CineMax.Application.Queries.GetMyTickets;
using CineMax.Application.Queries.GetTicketsPendingRepay;
using CineMax.Core.Entities;
using CineMax.Infra.Auth.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            {
                return NotFound("Tickets not found in database for that customer.");
            }

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

        [HttpPut("/repay/{ticketId})")]
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

        [HttpGet("/ticketspendingrepay")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetTicketsPendingRepay()
        {
            var query = new GetTicketsPendingRepayQuery();

            var TicketsPendingRepay = await _mediator.Send(query);

            if (TicketsPendingRepay is null)
                return BadRequest("Error fetching pending refund tickets.");

            return Ok(TicketsPendingRepay);
        }

        //[HttpPut("/confirmrepay/{ticketId})")]
        //[Authorize(Roles = Roles.Admin)]
        //public async Task<IActionResult> ConfirmRefundTicket([FromBody] int ticketId)
        //{

        //}
    }
}
