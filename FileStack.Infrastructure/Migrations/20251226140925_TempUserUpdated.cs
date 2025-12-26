using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TempUserUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "TempUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "TempUsers");
        }
    }
}
