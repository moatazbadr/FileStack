using FileStack.Application.APIResponses;
using MediatR;

namespace FileStack.Application.User.Command.UserProfilePicturePostCommand;

public class UserProfilePicturePostCommandHandler : IRequestHandler<UserProfilePicturePostCommand, UploadResponse>
{
    Task<UploadResponse> IRequestHandler<UserProfilePicturePostCommand, UploadResponse>.Handle(UserProfilePicturePostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
