namespace FileStack.Application.User;

public record CurrentUser(string UserId,string FirstName ,string LastName,DateOnly ? BirthDate ,string Email,IEnumerable<string>Roles)
{
    public bool isInRole(string role) => Roles.Contains(role);
}
