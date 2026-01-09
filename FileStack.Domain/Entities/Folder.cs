namespace FileStack.Domain.Entities;

public class Folder
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    //Navigation Properties
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;


    public int ? ParentFolderId { get; set; }
    public Folder? ParentFolder { get; set; }
        
    //subfolders
    public ICollection<Folder> ? SubFolders { get; set; } =new List<Folder>();
    public ICollection<FileEntity> ? Files { get; set; } = new List<FileEntity>();    



}
