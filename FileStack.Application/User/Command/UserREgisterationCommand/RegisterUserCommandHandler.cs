using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FileStack.Application.User.Command.UserREgisterationCommand
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponse>
    {
        private readonly IauthService _authService;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        public RegisterUserCommandHandler(IauthService authService ,ILogger<RegisterUserCommandHandler> logger )
        {
            _logger = logger;
            _authService =  authService;
        }

        public async Task<RegisterResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            
            var registerRequestDTO = new RegisterRequestDTO
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
                Password = request.Password,
            };
            _logger.LogInformation("User : {@User} Just Registered",request.Email);
            var response = await _authService.RegisterUserAsync(registerRequestDTO);
            return response;
        }
    }
}
