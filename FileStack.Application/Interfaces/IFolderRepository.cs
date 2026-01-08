using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;

namespace FileStack.Application.Interfaces;

public interface IFolderRepository
{
    Task<UploadResponse> CreateFolderAsync(string UserId, CreateFolderDto createFolder);

}
