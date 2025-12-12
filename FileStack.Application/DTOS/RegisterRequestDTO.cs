using System.ComponentModel.DataAnnotations;

namespace FileStack.Application.DTOS
{
    public class RegisterRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; } = DateOnly.MinValue;
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
