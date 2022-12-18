using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamidsAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Device_DeviceId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_SubjectSchedule_SubjectScheduleSchedId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectSchedule_Subjects_SubjectID",
                table: "SubjectSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectSchedule",
                table: "SubjectSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.RenameTable(
                name: "SubjectSchedule",
                newName: "SubjectSchedules");

            migrationBuilder.RenameTable(
                name: "Device",
                newName: "Devices");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectSchedule_SubjectID",
                table: "SubjectSchedules",
                newName: "IX_SubjectSchedules_SubjectID");

            migrationBuilder.AddColumn<int>(
                name: "Rfid",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectSchedules",
                table: "SubjectSchedules",
                column: "SchedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "DeviceId");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SchoolYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentID",
                table: "Users",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Devices_DeviceId",
                table: "Attendances",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_SubjectSchedules_SubjectScheduleSchedId",
                table: "Attendances",
                column: "SubjectScheduleSchedId",
                principalTable: "SubjectSchedules",
                principalColumn: "SchedId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectSchedules_Subjects_SubjectID",
                table: "SubjectSchedules",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Devices_DeviceId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_SubjectSchedules_SubjectScheduleSchedId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectSchedules_Subjects_SubjectID",
                table: "SubjectSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectSchedules",
                table: "SubjectSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Rfid",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "SubjectSchedules",
                newName: "SubjectSchedule");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "Device");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectSchedules_SubjectID",
                table: "SubjectSchedule",
                newName: "IX_SubjectSchedule_SubjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectSchedule",
                table: "SubjectSchedule",
                column: "SchedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Device_DeviceId",
                table: "Attendances",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_SubjectSchedule_SubjectScheduleSchedId",
                table: "Attendances",
                column: "SubjectScheduleSchedId",
                principalTable: "SubjectSchedule",
                principalColumn: "SchedId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectSchedule_Subjects_SubjectID",
                table: "SubjectSchedule",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }
    }
}
