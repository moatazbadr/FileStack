using FileStack.Application.APIResponses;
using MediatR;

namespace FileStack.Application.Folders.Commands.RenameFolder;

public class RenameFolderCommand : IRequest<UploadResponse>
{
    public int FolderId { get; set; }
    public string NewName { get; set; } = string.Empty;
}
