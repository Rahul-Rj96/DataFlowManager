CREATE TABLE [dbo].[RolePermissions] (
    [RolePermissionId] INT IDENTITY (1, 1) NOT NULL,
    [PermissionId]     INT NOT NULL,
    [RoleId]           INT NOT NULL,
    CONSTRAINT [PK_Constraint_RolePermissionId] PRIMARY KEY CLUSTERED ([RolePermissionId] ASC),
    CONSTRAINT [FK_Constraint_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Constraint_RoleId2] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE
);



