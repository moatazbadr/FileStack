
using FileStack.Application.APIResponses;
using Microsoft.AspNetCore.Http;

namespace FileStack.Infrastructure.Repositories;

public class UserProfileService  : IUserProfileService
{
    private bool DeleteFromStorage(string filePath)
    {
        //string path = Path.Combine("wwwroot", filePath.TrimStart('/'));
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;


    }
    public Task<UploadResponse> DeleteProfilePicture(string ? ImagePath)
    {
        var uploadResponse = new UploadResponse();
        if (ImagePath is null)
        {
            uploadResponse.FileUrl = "";
            uploadResponse.Message = "No Url was provided";
            uploadResponse.Success = false;
            return Task.FromResult(uploadResponse);
        }
         var isDeleted = DeleteFromStorage(ImagePath);
        if (isDeleted)
        {
            uploadResponse.FileUrl = ImagePath;
            uploadResponse.Message = "image deleted succesfully";
            uploadResponse.Success = true;
            return Task.FromResult(uploadResponse);

        }
        else
        {
            return Task.FromResult(new UploadResponse()
            {
                FileUrl = "",
                Message = "Couldn't delete the image",
                Success = false

            });
        }
        
    }

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
