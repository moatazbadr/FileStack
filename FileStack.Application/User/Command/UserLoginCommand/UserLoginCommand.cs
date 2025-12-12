using FileStack.Application.APIResponses;
using MediatR;

namespace FileStack.Application.User.Command.UserLoginCommand
{
    public class UserLoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
