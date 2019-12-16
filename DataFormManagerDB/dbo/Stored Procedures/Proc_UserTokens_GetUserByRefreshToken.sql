






CREATE PROCEDURE [dbo].[Proc_UserTokens_GetUserByRefreshToken]
    @RefreshToken nvarchar(max)
AS
BEGIN

SET NOCOUNT ON

	SELECT  * from RCKRUser
	WHERE userId in(
	SELECT userId from usertokens 
	WHERE tokenId in(
	SELECT tokenId from token 
	WHERE refreshtoken = @RefreshToken))
	
END