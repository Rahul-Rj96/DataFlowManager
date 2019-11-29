CREATE PROCEDURE [dbo].[Proc_RCKRUser_GetUser]
@UserId int
AS 
BEGIN
SELECT UserId,Username,EmailId,PhoneNo FROM RCKRUser 
WHERE UserId = @UserId
END