using CineMax.Application.Queries.GetAllMovies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/movies")]
    public class MoviesControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesControllers (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMoviesQuery();

           var movies = await _mediator.Send(query);

            return Ok(movies);
        }
    }
}

