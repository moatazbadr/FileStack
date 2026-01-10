using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;

namespace FileStack.Application.Interfaces;

public interface IStorageRepository <T> where T : class
{
    Task<UploadResponse> CreateFolderAsync(string UserId, CreateFolderDto createFolder);
    Task<UploadResponse> UploadFileAsync(string UserId, UploadFileDto uploadFile);
    Task<bool> renameFolder(RenameFolderDto dto);
    Task<T> getByIdAsync(int id);

}
