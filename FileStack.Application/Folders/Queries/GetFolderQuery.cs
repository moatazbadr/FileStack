using FileStack.Application.DTOS;
using MediatR;

namespace FileStack.Application.Folders.Queries;

public class GetFolderQuery : IRequest< IEnumerable< FolderToRturnDto>>
{
    public string FolderName { get; set; }
}
