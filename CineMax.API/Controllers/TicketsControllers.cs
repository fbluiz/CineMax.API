using CineMax.Application.Commands.BuyTicket;
using CineMax.Application.Queries.GetMyTickets;
using CineMax.Infra.Auth.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        //[HttpPost("/repay/{ticketId})")]
        //public async Task<IActionResult> RepayTicket(int id)
        //{
        //}
    }
}
