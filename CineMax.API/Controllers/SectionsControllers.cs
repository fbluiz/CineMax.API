using CineMax.Application.Commands.CreateSection;
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

            return Ok(Section);
        }
    }
}
