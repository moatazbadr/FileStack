using FileStack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FileStack.Application.User.Queries;

public class GetUserProfileQueryHandler(IUserContext _context,UserManager<ApplicationUser> userManager) : IRequestHandler<GetUserProfileQuery, UserProfile>
{
    public async Task<UserProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = _context.GetCurrentUser();
        var dbUser = await userManager.FindByEmailAsync(user.Email);
        if (dbUser == null) {
            return null;
        
        }
        var UserProfile =new UserProfile(dbUser.FirstName, dbUser.LastName, dbUser.BirthDate, dbUser.Email);
        return UserProfile;
        
    }
}
