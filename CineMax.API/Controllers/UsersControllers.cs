using CineMax.Core.DTOs.AuthDTOs.Requests;
using CineMax.Core.DTOs.AuthDTOs.Responses;
using CineMax.Core.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CineMax.API.Controllers
{
    [Route("api/users")]
    public class UsersControllers : ControllerBase
    {
        private IAuthService _authService;

        public UsersControllers(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("/register")]
        
        public async Task<ActionResult<RegisterUserResponse>> Register(RegisterUserRequest registerUser )
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            var result = await _authService.RegisterUser(registerUser);

            if (result.Success)
                return Ok(result);
            else if (result.Erros.Count > 0)
                return BadRequest(result);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login (UserLoginRequest userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _authService.Login(userLogin);
            if (result.Sucess) 
                return Ok(result);

            return Unauthorized(result);

        }
    }
}