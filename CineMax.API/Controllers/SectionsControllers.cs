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
            var Section = await _mediator.Send(command);
           
            if (Section == null)
                return BadRequest("A quantidade de tickets não pode ultrapasse a quantidade de cadeiras");

            return Ok(Section);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSections()
        {
            var query = new GetAllSectionsQuery();
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
