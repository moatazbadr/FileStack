using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFolderandfiletables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "FileEntities",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "FileEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StoredFileName",
                table: "FileEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "FileEntities");

            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "FileEntities");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FileEntities",
                newName: "FileName");
        }
    }
}
