CREATE PROCEDURE [dbo].[Proc_RCKRUser_UpdateUser]
@UserId int,
@EmailId varchar(255),
@PhoneNo varchar(15)
AS 
BEGIN
UPDATE RCKRUser
SET EmailId=@EmailId,PhoneNo=@PhoneNo
WHERE UserId = @UserId;
END