CREATE PROCEDURE Proc_RCKRUser_IsUserExist
    @Sub varchar(255)
AS
BEGIN

SET NOCOUNT ON

IF EXISTS(SELECT * FROM RCKRUser WHERE Sub = @Sub)
    SELECT 'true' AS UserExists
ELSE
    SELECT 'false' AS UserExists
END