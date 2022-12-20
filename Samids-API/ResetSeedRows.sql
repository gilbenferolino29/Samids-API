USE [SamidsDatabase]
GO

DBCC CHECKIDENT(Attendances, RESEED, 0)
DBCC CHECKIDENT(Devices, RESEED, 0)
DBCC CHECKIDENT(Faculties, RESEED, 0)
DBCC CHECKIDENT(Students, RESEED, 0)
DBCC CHECKIDENT(Subjects, RESEED, 0)
DBCC CHECKIDENT(SubjectSchedules, RESEED, 0)
DBCC CHECKIDENT(Users, RESEED, 0)
--TRUNCATE TABLE [dbo].[Attendances]
--TRUNCATE TABLE  [dbo].[Configs]
--TRUNCATE TABLE  [dbo].[Devices]	
--TRUNCATE TABLE  [dbo].[Faculties]
--TRUNCATE TABLE  [dbo].[FacultySubject]
--TRUNCATE TABLE [dbo].[Students]
--TRUNCATE TABLE  [dbo].[StudentSubject]
--TRUNCATE TABLE  [dbo].[Subjects]
--TRUNCATE TABLE  [dbo].[SubjectSchedules]
--TRUNCATE TABLE  [dbo].[Users]
GO


