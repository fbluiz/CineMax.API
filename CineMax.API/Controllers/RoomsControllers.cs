using CineMax.Application.Commands.CreateRoom;
using CineMax.Application.Commands.DeleteRoom;
using CineMax.Application.Commands.UpdateRoomCommand;
using CineMax.Application.Queries.GetAllRoom;
using CineMax.Application.Queries.GetRoomAndSectionById;
using CineMax.Infra.Auth.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/rooms")]
    public class RoomsControllers : CineMaxBaseController
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
                return BadRequest("Room not found in our database.");

            return Ok(roomAndSection);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<IActionResult> UpdateRoom([FromRoute]int id, [FromBody] UpdateRoomCommand command)
        {
            var roomId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetRoomAndSectionById), new { id = id }, command);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetRoomAndSectionById), new { id = id }, command);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteRoomCommand command)
        {
            if (command.Id != id)
                return BadRequest("The route id is different from the given id.");

            await _mediator.Send(command);

            return Ok("Update done successfully!");
        }
    }
}
