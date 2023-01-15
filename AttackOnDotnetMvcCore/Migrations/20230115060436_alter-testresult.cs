using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttackOnDotnetMvcCore.Migrations
{
    /// <inheritdoc />
    public partial class altertestresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestNumber",
                table: "TestResult");

            migrationBuilder.RenameColumn(
                name: "TechniqueID",
                table: "TestResult",
                newName: "TestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestID",
                table: "TestResult",
                newName: "TechniqueID");

            migrationBuilder.AddColumn<int>(
                name: "TestNumber",
                table: "TestResult",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
