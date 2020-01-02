CREATE PROCEDURE [dbo].[Proc_RCKRUser_GetUserToAssignForm]
AS 
BEGIN
SELECT UserId,Username FROM RCKRUser 
END