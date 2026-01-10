
using AutoMapper;
using FileStack.Application.User;

namespace FileStack.Infrastructure.Repositories;

public class StorageRepository(ApplicationDbContext _context, IMapper _mapper) : IStorageRepository
{
    public async Task<UploadResponse> CreateFolderAsync(string UserId ,CreateFolderDto dto)
    {
        var UploadResponse = new UploadResponse();
        if (dto.ParentFolderId.HasValue)
        {
            bool parentFolderExists = await _context.Folders.AnyAsync(f => f.Id == dto.ParentFolderId.Value && f.UserId == UserId);
            if (!parentFolderExists)
            {
                UploadResponse.Success = false;
                UploadResponse.Message = "Parent folder does not exist.";
                UploadResponse.FileUrl= string.Empty;
                return UploadResponse;
            }
            
        }
                var folder = new Folder()
                {
                    Name = dto.Name,
                    ParentFolderId = dto.ParentFolderId,
                    UserId = UserId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Folders.Add(folder);
                await _context.SaveChangesAsync();
                UploadResponse.Success = true;
                UploadResponse.Message = "Folder created successfully.";
                UploadResponse.FileUrl = $"folders/{folder.Id}";
                return UploadResponse;
    }

    public Task<bool> renameFolder(RenameFolderDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<UploadResponse> UploadFileAsync(string userId, UploadFileDto dto)
    {
        var folder = await _context.Folders
       .FirstOrDefaultAsync(f =>
           f.Id == dto.FolderId &&
           f.UserId == userId);

        if (folder == null)
        { 
            return new UploadResponse
            {
                Success = false,
                Message = "Folder does not exist",
                FileUrl = string.Empty
            }; 

        }

        var userPath = EnsureUserUploadFolder(userId);

        var extension = Path.GetExtension(dto.File.FileName);
        var storedFileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(userPath, storedFileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }
        var fileEntity = new FileEntity
        {
            OriginalFileName = dto.File.FileName,
            StoredFileName = storedFileName,
            FileUrl = $"/uploads/{userId}/{storedFileName}",
            Size = dto.File.Length,
            FolderId = folder.Id,
            UserId = userId,
        };

        _context.FileEntities.Add(fileEntity);
        await _context.SaveChangesAsync();
        return new UploadResponse
        {
            Success = true,
            Message = "File uploaded successfully",
            FileUrl = fileEntity.FileUrl
        };


    }


    private string EnsureUserUploadFolder(string userId)
    {
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads",
            userId
        );

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        return path;
    }
}
