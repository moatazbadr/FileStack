namespace FileStack.Application.DTOS;

public class CreateFolderDto
{

    public string Name { get; set; } =null!;
    public int? ParentFolderId { get; set; }

}
