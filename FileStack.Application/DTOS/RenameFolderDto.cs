namespace FileStack.Application.DTOS;

public class RenameFolderDto
{
    public int FolderId { get; set; }
    public string NewName { get; set; } = string.Empty;
}
