CREATE PROCEDURE [dbo].[Proc_Files_GetFile]
 @FileId int
 As
 begin
 select FileFullName, FileRecord,FileType from Files
 where FileId = @FileId
 end