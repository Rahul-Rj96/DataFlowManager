

CREATE PROCEDURE [dbo].[Proc_Token_GetToken]
@TokenId int
AS 
BEGIN
SELECT * FROM Token 
WHERE TokenId = @TokenId
END