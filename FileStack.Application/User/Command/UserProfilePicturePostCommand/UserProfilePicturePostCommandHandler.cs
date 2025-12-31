using FileStack.Application.APIResponses;
using FileStack.Application.Interfaces;
using FileStack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FileStack.Application.User.Command.UserProfilePicturePostCommand;

public class UserProfilePicturePostCommandHandler(IUserProfileService _userProfile,IUserContext _userContext ,UserManager<ApplicationUser> _userManager) : IRequestHandler<UserProfilePicturePostCommand, UploadResponse>
{
    async Task<UploadResponse> IRequestHandler<UserProfilePicturePostCommand, UploadResponse>.Handle(UserProfilePicturePostCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();
       if (user == null)
       {
                throw new Exception("User not found");
       }
       var dbUser = await  _userManager.FindByEmailAsync(user.Email);
       var uploadResult = await _userProfile.UploadUserPicture(request.ProfilePicture);

        dbUser.ProfileImageUrl = uploadResult.FileUrl;
        await _userManager.UpdateAsync(dbUser);

        return uploadResult;



    }
}
