using CineMax.API.Attributes;
using CineMax.Application.Queries;
using CineMax.Application.Queries.GetMyTickets;
using CineMax.Infra.Auth.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
        //[HttpPost("buy/{sectionId}")]
        //public async Task<IActionResult> BuyTickey(int id)
        //{
        //}

        //[HttpPost("/repay/{ticketId})")]
        //public async Task<IActionResult> RepayTicket(int id)
        //{
        //}
    }
}
