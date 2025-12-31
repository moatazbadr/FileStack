using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStack.Infrastructure.Configurations;

public class FolderEntityConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasOne(f => f.ParentFolder)
            .WithMany(f => f.SubFolders)
            .HasForeignKey(f => f.ParentFolderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(f=>f.User)
            .WithMany(u=>u.Folders)
            .HasForeignKey(f=>f.UserId)
            .OnDelete(DeleteBehavior.Cascade);



    }
}
