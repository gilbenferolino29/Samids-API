using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamidsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Databaseinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    CurrentTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LateMinutes = table.Column<int>(type: "int", nullable: false),
                    AbsentMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    StudentsStudentID = table.Column<int>(type: "int", nullable: false),
                    SubjectsSubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => new { x.StudentsStudentID, x.SubjectsSubjectID });
                    table.ForeignKey(
                        name: "FK_StudentSubject_Students_StudentsStudentID",
                        column: x => x.StudentsStudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subjects_SubjectsSubjectID",
                        column: x => x.SubjectsSubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectSchedule",
                columns: table => new
                {
                    SchedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectID = table.Column<int>(type: "int", nullable: true),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectSchedule", x => x.SchedId);
                    table.ForeignKey(
                        name: "FK_SubjectSchedule_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateTable(
                name: "FacultySubject",
                columns: table => new
                {
                    FacultiesFacultyId = table.Column<int>(type: "int", nullable: false),
                    SubjectsSubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultySubject", x => new { x.FacultiesFacultyId, x.SubjectsSubjectID });
                    table.ForeignKey(
                        name: "FK_FacultySubject_Subjects_SubjectsSubjectID",
                        column: x => x.SubjectsSubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultySubject_Teachers_FacultiesFacultyId",
                        column: x => x.FacultiesFacultyId,
                        principalTable: "Teachers",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    attendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    SubjectScheduleSchedId = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualTimeIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualTimeOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    remarks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.attendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId");
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                    table.ForeignKey(
                        name: "FK_Attendances_SubjectSchedule_SubjectScheduleSchedId",
                        column: x => x.SubjectScheduleSchedId,
                        principalTable: "SubjectSchedule",
                        principalColumn: "SchedId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DeviceId",
                table: "Attendances",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentID",
                table: "Attendances",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_SubjectScheduleSchedId",
                table: "Attendances",
                column: "SubjectScheduleSchedId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultySubject_SubjectsSubjectID",
                table: "FacultySubject",
                column: "SubjectsSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_SubjectsSubjectID",
                table: "StudentSubject",
                column: "SubjectsSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSchedule_SubjectID",
                table: "SubjectSchedule",
                column: "SubjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "FacultySubject");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "SubjectSchedule");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
