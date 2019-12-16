CREATE TABLE [dbo].[UserTokens] (
    [UserTokenId] INT IDENTITY (1, 1) NOT NULL,
    [UserId]      INT NULL,
    [TokenId]     INT NULL,
    CONSTRAINT [PK_Constraint_UserTokenId] PRIMARY KEY CLUSTERED ([UserTokenId] ASC),
    CONSTRAINT [FK_Constraint_TokenId] FOREIGN KEY ([TokenId]) REFERENCES [dbo].[Token] ([TokenId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Constraint_UserId_UserTokens] FOREIGN KEY ([UserId]) REFERENCES [dbo].[RCKRUser] ([UserId]) ON DELETE CASCADE
);

