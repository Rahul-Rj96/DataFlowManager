CREATE PROCEDURE [dbo].[Proc_RCKRUser_GetUserList]
AS 
BEGIN
SELECT UserId,Username,EmailId,PhoneNo FROM RCKRUser 
END