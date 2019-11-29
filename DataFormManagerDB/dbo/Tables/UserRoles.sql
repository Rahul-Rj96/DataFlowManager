CREATE TABLE [dbo].[UserRoles] (
    [UserRoleId] INT NOT NULL,
    [UserId]     INT NOT NULL,
    [RoleId]     INT NOT NULL,
    CONSTRAINT [PK_Constraint_UserRoleId] PRIMARY KEY CLUSTERED ([UserRoleId] ASC),
    CONSTRAINT [FK_Constraint_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]),
    CONSTRAINT [FK_Constraint_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[RCKRUser] ([UserId])
);

