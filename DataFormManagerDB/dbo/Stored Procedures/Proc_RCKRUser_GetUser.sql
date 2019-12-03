
CREATE PROCEDURE [dbo].[Proc_RCKRUser_GetUser]
@UserId int
AS 
BEGIN
SELECT * FROM RCKRUser 
WHERE UserId = @UserId
END