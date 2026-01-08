using Microsoft.AspNetCore.Http;

namespace FileStack.Application.DTOS;

public class UploadFileDto
{
    public IFormFile File { get; set; } = null!;
    public int FolderId { get; set; }
}
