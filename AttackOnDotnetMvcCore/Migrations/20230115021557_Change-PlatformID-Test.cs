using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttackOnDotnetMvcCore.Migrations
{
    /// <inheritdoc />
    public partial class ChangePlatformIDTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Test");

            migrationBuilder.AddColumn<int>(
                name: "PlatformID",
                table: "Test",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlatformID",
                table: "Test");

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
