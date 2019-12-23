CREATE TABLE [dbo].[UserRoles] (
    [UserRoleId] INT IDENTITY (1, 1) NOT NULL,
    [UserId]     INT NULL,
    [RoleId]     INT NULL,
    CONSTRAINT [PK_Constraint_UserRoleId] PRIMARY KEY CLUSTERED ([UserRoleId] ASC),
    CONSTRAINT [FK_Constraint_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Constraint_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[RCKRUser] ([UserId]) ON DELETE CASCADE
);



