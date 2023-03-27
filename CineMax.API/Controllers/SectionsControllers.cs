using CineMax.Application.Commands.CreateSection;
using CineMax.Application.Queries.GetAllRoom;
using CineMax.Application.Queries.GetAllSections;
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
        //[HttpGet]
        //public async Task<IActionResult> GetSectionsDisponible()
        //{
        //    var query = new GetSectionsDisponibleQuery();
        //    var sections = await _mediator.Send(query);
        //}
    }
}
