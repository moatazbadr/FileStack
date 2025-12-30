using FileStack.Application.APIResponses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileStack.Application.User.Command.UserProfilePicturePostCommand;

public class UserProfilePicturePostCommand :IRequest<UploadResponse>
{
    public IFormFile ? ProfilePicture { get; set; }

    public string ? userId { get; set; }


}
