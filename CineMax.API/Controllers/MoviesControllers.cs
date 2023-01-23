using CineMax.Application.Commands.CreateMovie;
using CineMax.Application.Queries.GetAllMovies;
using CineMax.Application.Queries.GetMovieById;
using CineMax.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/movies")]
    public class MoviesControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesControllers(IMediator mediator)
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

        [HttpGet("{id})")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMovieByIdQuery(id);

            var movie = await _mediator.Send(query);

            if (movie == null)
                return BadRequest("Filme não encontrado na nossa base de dados");

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovieCommand commannd)
        {
            var id = await _mediator.Send(commannd);

            return CreatedAtAction(nameof(GetById), new {id = id}, commannd);
        }
    }
}

