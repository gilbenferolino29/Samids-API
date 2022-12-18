using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamidsAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultySubject_Teachers_FacultiesFacultyId",
                table: "FacultySubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Faculties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultySubject_Faculties_FacultiesFacultyId",
                table: "FacultySubject",
                column: "FacultiesFacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultySubject_Faculties_FacultiesFacultyId",
                table: "FacultySubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.RenameTable(
                name: "Faculties",
                newName: "Teachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultySubject_Teachers_FacultiesFacultyId",
                table: "FacultySubject",
                column: "FacultiesFacultyId",
                principalTable: "Teachers",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
