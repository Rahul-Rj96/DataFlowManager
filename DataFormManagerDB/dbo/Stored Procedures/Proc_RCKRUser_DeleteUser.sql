
CREATE PROCEDURE [dbo].[Proc_RCKRUser_DeleteUser]
@UserId int
AS 
BEGIN
DELETE FROM RCKRUser
WHERE UserId=@UserId 
END