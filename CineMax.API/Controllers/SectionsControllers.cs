using CineMax.Application.Commands.CreateSection;
using CineMax.Application.Commands.DeleteMovieCommand;
using CineMax.Application.Commands.DeleteRoom;
using CineMax.Application.Commands.UpdateSection;
using CineMax.Application.Queries.GetAllSections;
using CineMax.Application.Queries.GetSectionById;
using CineMax.Infra.Auth.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/sections")]
    [Authorize]
    public class SectionsControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public SectionsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateSection([FromBody] CreateSectionCommand command)
        {
            var sectionViewModel = await _mediator.Send(command);

            if (sectionViewModel == null)
                return BadRequest("The number of tickets cannot exceed the number of chairs.");

            return Ok(sectionViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSections([FromQuery] GetAllSectionsQuery query)
        {
            var sections = await _mediator.Send(query);

            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSections(int id)
        {
            var query = new GetSectionByIdQuery
            {
                Id = id
            };

            var sections = await _mediator.Send(query);

            if(sections == null)
              return NotFound("No objects referenced with that id were found in the database. id = " + id);

            return Ok(sections);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateSection([FromRoute] int id, [FromBody] UpdateSectionCommand command)
        {
            if (id != command.Id)
                return BadRequest("The route id is different from the given id.");

            var sectionId = await _mediator.Send(command);

            var sectionViewModel = await GetByIdSections(sectionId);

            return CreatedAtAction(nameof(GetByIdSections), new { id = sectionId }, command);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteSectionCommand command)
        {
            if (command.Id != id)
                return BadRequest("The route id is different from the given id.");

            await _mediator.Send(command);

            return Ok("Update done successfully!");
        }
    }
}
