using CineMax.Core.DTOs.AuthDTOs.Requests;
using CineMax.Core.DTOs.AuthDTOs.Responses;
using CineMax.Core.Repositories;
using MediatR;
using CineMax.Core.Entities;
using CineMax.Core.Interfaces;

namespace CineMax.Application.Commands.CreateUserAndClient
{
    public class CreateUserAndClientCommandHandler : IRequestHandler<CreateUserAndClientCommand, RegisterUserResponse>
    {
        private readonly IAuthService _authService;
        private readonly IClientRepository _ClientRepository;

        public CreateUserAndClientCommandHandler(IAuthService authService, IClientRepository clientRepository)
        {
            _authService = authService;
            _ClientRepository = clientRepository;
        }

        public async Task<RegisterUserResponse> Handle(CreateUserAndClientCommand request, CancellationToken cancellationToken)
        {
            var registerUser = new RegisterUserRequest
            {
                Email = request.Email,
                Password = request.Password,
                PasswordConfirmation = request.PasswordConfirmation,
                Role = request.Role,
                Name = request.FullName            };

            var registerUserResponse = await _authService.RegisterUser(registerUser);

            if (registerUserResponse.Success)
            {
                var client = new Client(registerUser.Name,registerUser.Email,registerUserResponse.Role,registerUserResponse.UserId);
                await _ClientRepository.AddAsync(client);
            } 

            return registerUserResponse;
        }
    }
}
