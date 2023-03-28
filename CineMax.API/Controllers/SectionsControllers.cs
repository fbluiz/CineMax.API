using CineMax.Application.Commands.CreateSection;
using CineMax.Application.Queries.GetAllRoom;
using CineMax.Application.Queries.GetAllSections;
using CineMax.Application.Queries.GetSectionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/sections")]
    public class SectionsControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public SectionsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
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
        public async Task<IActionResult> GetByIdSections([FromRoute] int id)
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
    }
}
