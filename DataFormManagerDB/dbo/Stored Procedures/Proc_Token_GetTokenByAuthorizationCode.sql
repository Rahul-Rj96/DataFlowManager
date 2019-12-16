


CREATE PROCEDURE [dbo].[Proc_Token_GetTokenByAuthorizationCode]
@AuthorizationCode nvarchar(max)
AS 
BEGIN
SELECT * FROM Token 
WHERE AuthorizationCode = @AuthorizationCode
END