namespace FileStack.Domain.Entities
{
    public class FileEntity
    {
        public int Id { get; set; }

        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;

        //cannot be null if the file is in An already created folder
        public int FolderId { get; set; }
        public Folder Folder { get; set; } = null!;
    }
}
