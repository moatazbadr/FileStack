using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using FileStack.Application.User;
using MediatR;

namespace FileStack.Application.Folders.Commands.CreateFolder;

public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, UploadResponse>
{
    private readonly IStorageRepository _storageRepository;
    private readonly IUserContext _userService;
    public CreateFolderCommandHandler(IStorageRepository storageRepository, IUserContext userService)
    {
        _storageRepository = storageRepository;
        _userService = userService;
    }
    public async Task<UploadResponse> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        var user = _userService.GetCurrentUser();
        if (user == null) { 
                
            return new UploadResponse
            {
                Success = false,
                Message = "User not found.",
                FileUrl = string.Empty
            };

        }
        var createFolderDto = new CreateFolderDto
        {
            Name = request.Name,
            ParentFolderId = request.ParentFolderId
        };
        var result = await _storageRepository.CreateFolderAsync(user.UserId, createFolderDto);
        return result;
    }
}
