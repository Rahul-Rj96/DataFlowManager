

CREATE PROCEDURE [dbo].[Proc_Token_DeleteToken]
@TokenId int
AS 
BEGIN
DELETE FROM Token
WHERE TokenId=@TokenId
END