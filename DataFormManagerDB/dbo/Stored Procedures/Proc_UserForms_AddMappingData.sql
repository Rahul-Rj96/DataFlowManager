create PROCEDURE [dbo].[Proc_UserForms_AddMappingData]
@FormId int,
@UserId int
AS 
BEGIN
Insert into UserForms(FormId,UserId)
values(@FormId,@UserId)
END