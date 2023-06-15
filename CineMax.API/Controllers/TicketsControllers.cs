using CineMax.API.Attributes;
using CineMax.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/tickets")]
    [Authorize]
    public class TicketsControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ServiceFilter(typeof(ExtractUserIdFilter))]
        public async Task<IActionResult> MyTickets(GetMyTicketsQuery query)
        {
            var tickets = await _mediator.Send(query);

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
