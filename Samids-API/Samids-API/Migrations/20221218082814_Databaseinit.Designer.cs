﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Samids_API.Data;

#nullable disable

namespace SamidsAPI.Migrations
{
    [DbContext(typeof(SamidsDataContext))]
    [Migration("20221218082814_Databaseinit")]
    partial class Databaseinit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FacultySubject", b =>
                {
                    b.Property<int>("FacultiesFacultyId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsSubjectID")
                        .HasColumnType("int");

                    b.HasKey("FacultiesFacultyId", "SubjectsSubjectID");

                    b.HasIndex("SubjectsSubjectID");

                    b.ToTable("FacultySubject");
                });

            modelBuilder.Entity("Samids_API.Models.Attendance", b =>
                {
                    b.Property<int>("attendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("attendanceId"));

                    b.Property<DateTime?>("ActualTimeIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ActualTimeOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeviceId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentID")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectScheduleSchedId")
                        .HasColumnType("int");

                    b.Property<int>("remarks")
                        .HasColumnType("int");

                    b.HasKey("attendanceId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("StudentID");

                    b.HasIndex("SubjectScheduleSchedId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Samids_API.Models.Config", b =>
                {
                    b.Property<int>("AbsentMinutes")
                        .HasColumnType("int");

                    b.Property<string>("CurrentTerm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CurrentYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("LateMinutes")
                        .HasColumnType("int");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("Samids_API.Models.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeviceId"));

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("Samids_API.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacultyId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacultyId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Samids_API.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"));

                    b.Property<string>("Course")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("StudentID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Samids_API.Models.Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectID"));

                    b.Property<string>("SubjectDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Samids_API.Models.SubjectSchedule", b =>
                {
                    b.Property<int>("SchedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchedId"));

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubjectID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime2");

                    b.HasKey("SchedId");

                    b.HasIndex("SubjectID");

                    b.ToTable("SubjectSchedule");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.Property<int>("StudentsStudentID")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsSubjectID")
                        .HasColumnType("int");

                    b.HasKey("StudentsStudentID", "SubjectsSubjectID");

                    b.HasIndex("SubjectsSubjectID");

                    b.ToTable("StudentSubject");
                });

            modelBuilder.Entity("FacultySubject", b =>
                {
                    b.HasOne("Samids_API.Models.Faculty", null)
                        .WithMany()
                        .HasForeignKey("FacultiesFacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Samids_API.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Samids_API.Models.Attendance", b =>
                {
                    b.HasOne("Samids_API.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");

                    b.HasOne("Samids_API.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID");

                    b.HasOne("Samids_API.Models.SubjectSchedule", "SubjectSchedule")
                        .WithMany()
                        .HasForeignKey("SubjectScheduleSchedId");

                    b.Navigation("Device");

                    b.Navigation("Student");

                    b.Navigation("SubjectSchedule");
                });

            modelBuilder.Entity("Samids_API.Models.SubjectSchedule", b =>
                {
                    b.HasOne("Samids_API.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.HasOne("Samids_API.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Samids_API.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}