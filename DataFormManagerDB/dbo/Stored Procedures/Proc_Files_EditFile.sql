CREATE PROCEDURE [dbo].[Proc_Files_EditFile]
@FileRecord varbinary(max),
@FileType varchar(500),
@FileFullName varchar(500),
@FileFormType varchar(500),
@FileId int
AS 
BEGIN
Update Files
Set FileRecord = @FileRecord , FileType = @FileType , FileFullName = @FileFullName , FileFormType = @FileFormType
where Files.FileId = @FileId
END