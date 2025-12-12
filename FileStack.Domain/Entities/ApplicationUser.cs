using Microsoft.AspNetCore.Identity;

namespace FileStack.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateOnly BirthDate { get; set; }

}
