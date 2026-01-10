using AutoMapper;
using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using FileStack.Application.User;
using MediatR;

namespace FileStack.Application.Folders.Commands.CreateFolder;

public class CreateFolderCommandHandler(IStorageRepository _storageRepository, IUserContext _userService ,IMapper mapper) : IRequestHandler<CreateFolderCommand, UploadResponse>
{
   
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
        var createFolderDto = mapper.Map<CreateFolderDto>(request);
        var result = await _storageRepository.CreateFolderAsync(user.UserId, createFolderDto);
        return result;
    }
}
