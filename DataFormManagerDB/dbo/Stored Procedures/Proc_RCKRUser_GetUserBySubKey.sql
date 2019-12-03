

CREATE PROCEDURE [dbo].[Proc_RCKRUser_GetUserBySubKey]
@Sub varchar(255)
AS 
BEGIN
SELECT * FROM RCKRUser 
WHERE Sub = @Sub
END