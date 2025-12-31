using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStack.Infrastructure.Configurations;

public class FileEntityConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.HasOne(f=>f.Folder)
            .WithMany(f=>f.Files)
            .HasForeignKey(f=>f.FolderId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
