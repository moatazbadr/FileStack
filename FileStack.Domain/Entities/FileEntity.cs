namespace FileStack.Domain.Entities
{
    public class FileEntity
    {
        public int Id { get; set; }

        public string OriginalFileName { get; set; } = null!;
        public string StoredFileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;

        //cannot be null if the file is in An already created folder
        public long Size { get; set; }
        public int FolderId { get; set; }
        public Folder Folder { get; set; } = null!;

        public string UserId { get; set; } = null!;

    }
}
