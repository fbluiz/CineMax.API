using CineMax.Application.Commands.CreateUserAndClient;
using CineMax.Core.DTOs.AuthDTOs.Requests;
using CineMax.Core.DTOs.AuthDTOs.Responses;
using CineMax.Core.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/user")]
    public class UsersControllers : ControllerBase
    {
        private readonly IMediator _mediator;
        private IAuthService _authService;

        public UsersControllers(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpPost("/register")]

        public async Task<ActionResult<RegisterUserResponse>> Register(CreateUserAndClientCommand createUserAndClientCommand)
        {
            var registerUserResponse = await _mediator.Send(createUserAndClientCommand);

            if (!ModelState.IsValid)
                return BadRequest();

             if (registerUserResponse.Success)
                 return Ok(registerUserResponse);
             else if (registerUserResponse.Erros.Count > 0)
               return BadRequest(registerUserResponse);

             return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("/login")]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest userLogin)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();

            var result = await _authService.Login(userLogin);
            if (result.Sucess)
                return Ok(result);

            return Unauthorized(result);

        }
    }
}