namespace FileStack.Application.User;

public record UserProfile(string FirstName, string LastName, DateOnly? BirthDate, string Email)
{

}
