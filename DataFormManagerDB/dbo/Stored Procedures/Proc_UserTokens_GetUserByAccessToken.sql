







CREATE PROCEDURE [dbo].[Proc_UserTokens_GetUserByAccessToken]
    @AccessToken nvarchar(max)
AS
BEGIN

SET NOCOUNT ON

	SELECT  * from RCKRUser
	WHERE userId in(
	SELECT userId from usertokens 
	WHERE tokenId in(
	SELECT tokenId from token 
	WHERE accesstoken = @AccessToken))
	
END