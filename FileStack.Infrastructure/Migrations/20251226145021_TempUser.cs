using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TempUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "TempUsers",
                newName: "PasswordPlain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordPlain",
                table: "TempUsers",
                newName: "PasswordHash");
        }
    }
}
