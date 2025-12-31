using System.ComponentModel.DataAnnotations;

namespace FileStack.Domain.Entities
{
    public class TempUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        

        public DateTime BirthDate { get; set; } = DateTime.MinValue;
        public string Email { get; set; }

        public string PasswordPlain { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;


    }
}
