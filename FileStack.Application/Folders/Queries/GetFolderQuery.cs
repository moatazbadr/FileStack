using FileStack.Application.DTOS;
using MediatR;

namespace FileStack.Application.Folders.Queries;

public class GetFolderQuery : IRequest<FolderToRturnDto>
{
    public int FolderId { get; set; }
}
