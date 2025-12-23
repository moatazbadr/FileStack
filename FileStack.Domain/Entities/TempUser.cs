using System.ComponentModel.DataAnnotations;

namespace FileStack.Domain.Entities
{
    public class TempUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        

        public DateOnly BirthDate { get; set; } = DateOnly.MinValue;
        public string Email { get; set; }

        public string PasswordHash { get; set; } = string.Empty;


    }
}
