



CREATE PROCEDURE [dbo].[Proc_UserTokens_CreateUserToken]
@UserId int,
@TokenId int
AS
BEGIN
INSERT INTO UserTokens (UserId,TokenId) 
Values (@UserId,@TokenId)
END