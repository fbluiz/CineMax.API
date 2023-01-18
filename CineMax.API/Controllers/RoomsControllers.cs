using CineMax.Application.Queries.GetAllRoom;
using CineMax.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/rooms")]
    public class RoomsControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomAndSectionAll()
        {
            var query = new GetAllRoomAndSectionQuery();

            var roomAndSections = await _mediator.Send(query);

            return Ok(roomAndSections);
        }
    }
}
