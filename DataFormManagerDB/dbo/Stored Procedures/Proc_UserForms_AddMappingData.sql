create PROCEDURE [dbo].[Proc_UserForms_AddMappingData]
(@UserFormsTable as dbo.UserFormsTable readonly)
AS 
BEGIN
Insert into UserForms(UserId,FormId)
select UserId, FormId
from @UserFormsTable
END