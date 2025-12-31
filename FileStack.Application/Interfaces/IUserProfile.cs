using FileStack.Application.APIResponses;
using Microsoft.AspNetCore.Http;

namespace FileStack.Application.Interfaces;

public interface IUserProfileService
{
    Task<UploadResponse> UploadUserPicture(IFormFile ? profilePicture);
}
