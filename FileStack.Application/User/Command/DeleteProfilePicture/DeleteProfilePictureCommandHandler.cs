using FileStack.Application.APIResponses;
using FileStack.Application.Interfaces;
using FileStack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FileStack.Application.User.Command.DeleteProfilePicture;

public class DeleteProfilePictureCommandHandler(IUserProfileService _profileService ,UserManager<ApplicationUser>_userManager,IUserContext _userContext) : IRequestHandler<DeleteProfilePictureCommand, UploadResponse>
{
    public async Task<UploadResponse> Handle(DeleteProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();
        UploadResponse uploadResponse = new UploadResponse();
        if (user == null)
        {
            uploadResponse.FileUrl = "";
            uploadResponse.Message = "User Was not found";
            uploadResponse.Success = false;
            return uploadResponse;
        }
        var dbUser = await _userManager.FindByEmailAsync(user.Email);
        if (dbUser == null) {
            uploadResponse.FileUrl = "";
            uploadResponse.Message = "User Was not found";
            uploadResponse.Success = false;
            return uploadResponse;
        }
        var IsProfileImageDeleted = _profileService.DeleteProfilePicture(dbUser!.ProfileImageUrl);
        if (IsProfileImageDeleted.Result.Success)
        {
            dbUser.ProfileImageUrl = null;
            await _userManager.UpdateAsync(dbUser);
            return IsProfileImageDeleted.Result;
        }
       else
        {
            uploadResponse.FileUrl = "";
            uploadResponse.Message = "Failed to Delete ";
            uploadResponse.Success = false;
            return uploadResponse;

        }
       
        
    }
}
