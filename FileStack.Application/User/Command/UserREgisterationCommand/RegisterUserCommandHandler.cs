using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using MediatR;

namespace FileStack.Application.User.Command.UserREgisterationCommand
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponse>
    {
        private readonly IauthService _authService;
        public RegisterUserCommandHandler(IauthService authService)
        {
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
            var response = await _authService.RegisterUserAsync(registerRequestDTO);
            return response;
        }
    }
}
