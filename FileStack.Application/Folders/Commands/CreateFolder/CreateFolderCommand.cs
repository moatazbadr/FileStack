using FileStack.Application.APIResponses;
using MediatR;

namespace FileStack.Application.Folders.Commands.CreateFolder;

public class CreateFolderCommand :IRequest<UploadResponse>
{
    public string Name { get; set; } = null!;
    public int? ParentFolderId { get; set; }
}
