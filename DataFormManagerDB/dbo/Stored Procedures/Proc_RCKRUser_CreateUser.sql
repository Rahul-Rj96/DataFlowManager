CREATE PROCEDURE [dbo].[Proc_RCKRUser_CreateUser]
@UserId int,
@Username varchar(255),
@Password varchar(255),
@EmailId varchar(255),
@PhoneNo varchar(15)
AS 
BEGIN
INSERT INTO RCKRUser (UserId,Username,Password,EmailId,PhoneNo) 
Values (@UserId,@Username,@Password,@EmailId,@PhoneNo)
END