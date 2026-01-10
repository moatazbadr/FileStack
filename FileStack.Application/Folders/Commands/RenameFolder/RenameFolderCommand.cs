using MediatR;

namespace FileStack.Application.Folders.Commands.RenameFolder;

public class RenameFolderCommand : IRequest<bool>
{
    public int FolderId { get; set; }
    public string NewName { get; set; } = string.Empty;
}
