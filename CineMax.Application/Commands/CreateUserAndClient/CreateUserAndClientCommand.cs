using CineMax.Core.DTOs.AuthDTOs.Responses;
using MediatR;

namespace CineMax.Application.Commands.CreateUserAndClient
{
    public class CreateUserAndClientCommand : IRequest<RegisterUserResponse>  
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
    }
}
