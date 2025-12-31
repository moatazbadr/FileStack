using FileStack.Application.APIResponses;
using MediatR;

namespace FileStack.Application.User.Command.DeleteProfilePicture;

public class DeleteProfilePictureCommand :IRequest<UploadResponse>
{
}
