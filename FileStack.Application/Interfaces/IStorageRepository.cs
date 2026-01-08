using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;

namespace FileStack.Application.Interfaces;

public interface IStorageRepository
{
    Task<UploadResponse> CreateFolderAsync(string UserId, CreateFolderDto createFolder);
    Task<UploadResponse> UploadFileAsync(string UserId, UploadFileDto uploadFile);


}
