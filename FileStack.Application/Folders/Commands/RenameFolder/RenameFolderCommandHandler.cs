using MediatR;

namespace FileStack.Application.Folders.Commands.RenameFolder;

public class RenameFolderCommandHandler : IRequestHandler<RenameFolderCommand, bool>
{
    public Task<bool> Handle(RenameFolderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
