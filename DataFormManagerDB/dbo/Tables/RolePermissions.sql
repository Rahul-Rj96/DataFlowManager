CREATE TABLE [dbo].[RolePermissions] (
    [PermissionId]     INT NOT NULL,
    [RolePermissionId] INT NOT NULL,
    [RoleId]           INT NOT NULL,
    CONSTRAINT [PK_Constraint_RolePermissionId] PRIMARY KEY CLUSTERED ([RolePermissionId] ASC),
    CONSTRAINT [FK_Constraint_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId]),
    CONSTRAINT [FK_Constraint_RoleId2] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);

