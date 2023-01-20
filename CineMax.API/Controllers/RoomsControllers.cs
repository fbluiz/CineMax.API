using CineMax.Application.Queries.GetAllRoom;
using CineMax.Application.Queries.GetRoomAndSectionById;
using CineMax.Application.ViewModels;
using CineMax.Core.Entities;
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

            var roomsAndSections = await _mediator.Send(query);

            return Ok(roomsAndSections);
        }

        [HttpGet("{id})")]
        public async Task<IActionResult> GetRoomAndSectionById(int id)
        {
            var query = new GetRoomAndSectionByIdQuery(id);

            var roomAndSection = await _mediator.Send(query);

            if (roomAndSection == null)
                return BadRequest("Sala não encontrada na nossa base de dados");

            return Ok(roomAndSection);
        }
    }
}
