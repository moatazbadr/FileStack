using Microsoft.AspNetCore.Identity;

namespace FileStack.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string? ProfileImageUrl { get; set; }

    public ICollection<Folder> Folders { get; set; } = new List<Folder>();



}
