using AutoMapper;
using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using MediatR;

namespace FileStack.Application.Folders.Commands.RenameFolder;

public class RenameFolderCommandHandler(IStorageRepository storage,IMapper _mapper) : IRequestHandler<RenameFolderCommand, UploadResponse>
{


    async Task<UploadResponse> IRequestHandler<RenameFolderCommand, UploadResponse>.Handle(RenameFolderCommand request, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<RenameFolderDto>(request);
        var result =  await storage.renameFolder(dto);
        if (!result)
        {
            return new UploadResponse
            {
                Success = false,
                Message = "Folder not found or could not be renamed.",
                FileUrl = string.Empty
            };
        }
        return new UploadResponse
        {
            Success = true,
            Message = "Folder renamed successfully.",
            FileUrl = $"folders/{request.FolderId}"
        };

    }
}
