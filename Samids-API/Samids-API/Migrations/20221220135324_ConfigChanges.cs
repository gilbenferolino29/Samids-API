using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamidsAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrentTerm",
                table: "Configs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configs",
                table: "Configs",
                column: "CurrentTerm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Configs",
                table: "Configs");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentTerm",
                table: "Configs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
