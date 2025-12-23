using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class passwordBugfixedintempuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "TempUsers");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "TempUsers",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "TempUsers",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "TempUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
