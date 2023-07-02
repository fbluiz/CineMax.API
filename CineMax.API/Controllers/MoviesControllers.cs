﻿using CineMax.Application.Commands.CreateMovie;
using CineMax.Application.Commands.DeleteMovieCommand;
using CineMax.Application.Commands.UpdateMovie;
using CineMax.Application.Queries.GetAllMovies;
using CineMax.Application.Queries.GetMovieById;
using CineMax.Infra.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/movies")]
    public class MoviesControllers : CineMaxBaseController
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
                return BadRequest("Film not found in our database.");

            return Ok(movie);
        }

        [HttpPost]
        [Authorize (Roles = Roles.Admin)]

        public async Task<IActionResult> Post([FromBody] CreateMovieCommand commannd)
        {
            var id = await _mediator.Send(commannd);

            return CreatedAtAction(nameof(GetById), new { id = id }, commannd);
        }

        [HttpPut("{id})")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateMovieCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route id is different from Json id");
            await _mediator.Send(command);

            if (command == null) 
                return BadRequest("There was an error trying to update the content.");

            return Ok("Update done successfully!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] DeleteMovieCommand command)
        {
            if (command.Id != id)
                return BadRequest("The route id is different from the given id.");

            await _mediator.Send(command);

            return Ok("Update done successfully!");
        }

    }
}