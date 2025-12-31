
using Microsoft.AspNetCore.Http;

namespace FileStack.Infrastructure.Repositories;

public class UserProfileService  : IUserProfileService
{
    public Task<UploadResponse> UploadUserPicture(IFormFile? profilePicture)
    {
        string fileName = Guid.NewGuid().ToString() + "_" + profilePicture?.FileName;
        string filePath = Path.Combine("wwwroot", "UserProfiles", fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            profilePicture?.CopyTo(stream);
        }
        return Task.FromResult(new UploadResponse
        {
            FileUrl = filePath,
            Message = "Profile picture uploaded successfully",
            Success = true
        });


    }
}
