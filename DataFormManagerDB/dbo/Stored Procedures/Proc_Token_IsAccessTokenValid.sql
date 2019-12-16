




CREATE PROCEDURE [dbo].[Proc_Token_IsAccessTokenValid]
    @AccessToken nvarchar(max)
AS
BEGIN

SET NOCOUNT ON

IF EXISTS(SELECT * FROM Token WHERE AccessToken = @AccessToken and ExpiresIn  > GETDATE())
	BEGIN
    SELECT 'true' AS AccessTokenValid
	END
ELSE
	BEGIN
    SELECT 'false' AS AccessTokenValid
	END
END