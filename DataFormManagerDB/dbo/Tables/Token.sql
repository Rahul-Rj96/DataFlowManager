CREATE TABLE [dbo].[Token] (
    [TokenId]           INT            IDENTITY (1, 1) NOT NULL,
    [AccessToken]       NVARCHAR (MAX) NOT NULL,
    [RefreshToken]      NVARCHAR (MAX) NOT NULL,
    [ExpiresIn]         DATETIME       NOT NULL,
    [AuthorizationCode] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Constraint_TokenId] PRIMARY KEY CLUSTERED ([TokenId] ASC)
);

