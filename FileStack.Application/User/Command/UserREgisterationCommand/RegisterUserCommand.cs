using FileStack.Application.APIResponses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FileStack.Application.User.Command.UserREgisterationCommand;

public class RegisterUserCommand : IRequest<RegisterResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; } = DateTime.MinValue;
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}
