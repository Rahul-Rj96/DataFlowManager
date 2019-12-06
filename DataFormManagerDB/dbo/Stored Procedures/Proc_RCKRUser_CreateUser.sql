
CREATE PROCEDURE [dbo].[Proc_RCKRUser_CreateUser]
@Username varchar(255),
@EmailId varchar(255),
@Sub varchar(255)
AS
BEGIN
INSERT INTO RCKRUser (Username,EmailId,Sub) 
Values (@Username,@EmailId,@Sub)
END