CREATE PROCEDURE [dbo].[Proc_Files_InsertFile]
@FileRecord varbinary(max),
@FileType varchar(500),
@FileFullName varchar(500),
@FileFormType varchar(500)
AS 
BEGIN
Insert into Files(FileRecord,FileType,FileFullName,FileFormType)
values(@FileRecord,@FileType,@FileFullName,@FileFormType)
select  SCOPE_IDENTITY() as FileId;
END