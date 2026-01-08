
using FileStack.Application.User;

namespace FileStack.Infrastructure.Repositories;

public class FolderRepository(ApplicationDbContext _context) : IFolderRepository
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
                var folder = new Folder
                {
                    Name = dto.Name,
                    ParentFolderId = dto.ParentFolderId,
                    UserId = UserId
                };
                _context.Folders.Add(folder);
                await _context.SaveChangesAsync();
                UploadResponse.Success = true;
                UploadResponse.Message = "Folder created successfully.";
                UploadResponse.FileUrl = $"folders/{folder.Id}";
                return UploadResponse;
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
