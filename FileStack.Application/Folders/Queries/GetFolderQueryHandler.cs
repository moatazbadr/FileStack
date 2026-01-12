using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using FileStack.Application.User;
using MediatR;

namespace FileStack.Application.Folders.Queries;

public class GetFolderQueryHandler(IStorageRepository repository, IUserContext _UserContext) : IRequestHandler<GetFolderQuery, IEnumerable<FolderToRturnDto>>
{
    public async Task<IEnumerable<FolderToRturnDto>> Handle(GetFolderQuery request, CancellationToken cancellationToken)
    {
        var user = _UserContext.GetCurrentUser();
        if (user == null)
        {
            return Enumerable.Empty<FolderToRturnDto>();
        }
        var folders = await repository.getByNameAsync( user.UserId, request.FolderName);
        return folders;


    }
}
