using MediatR;

namespace FileStack.Application.User.Queries;

public class GetUserProfileQuery : IRequest<UserProfile>
{
}
