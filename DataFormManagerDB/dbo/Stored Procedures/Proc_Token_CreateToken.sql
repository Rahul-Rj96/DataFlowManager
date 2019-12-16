


CREATE PROCEDURE [dbo].[Proc_Token_CreateToken]
@AccessToken nvarchar(max),
@RefreshToken nvarchar(max),
@ExpiresIn datetime,
@AuthorizationCode nvarchar(max)
AS
BEGIN
INSERT INTO Token (AccessToken,RefreshToken,ExpiresIn,AuthorizationCode) 
Values (@AccessToken,@RefreshToken,@ExpiresIn,@AuthorizationCode)
END