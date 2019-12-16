




CREATE PROCEDURE [dbo].[Proc_Token_IsRefreshTokenValid]
    @RefreshToken nvarchar(max)
AS
BEGIN

SET NOCOUNT ON

IF EXISTS(SELECT * FROM Token WHERE RefreshToken = @RefreshToken and ExpiresIn  > cast(GETDATE() as date))
	BEGIN
	select 'true' AS RefreshTokenValid, userId from usertokens 
	where tokenId in(
	select tokenId from token 
	where refreshtoken = @RefreshToken)
	DELETE from Token WHERE RefreshToken = @RefreshToken
	END
ELSE
	BEGIN
	DELETE from Token WHERE RefreshToken = @RefreshToken
    select 'false' AS RefreshTokenValid, 0 AS userId 
	END
END