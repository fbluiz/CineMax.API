using CineMax.Application.Commands.CreateRoom;
using CineMax.Application.Commands.UpdateRoomCommand;
using CineMax.Application.Queries.GetAllRoom;
using CineMax.Application.Queries.GetRoomAndSectionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    //teste
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
                return BadRequest("Sala não encontrada na nossa base de dados ");

            return Ok(roomAndSection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom([FromRoute]int id, [FromBody] UpdateRoomCommand command)
        {
            var roomId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetRoomAndSectionById), new { id = id }, id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetRoomAndSectionById), new { id = id }, command);
        }
    }
}
