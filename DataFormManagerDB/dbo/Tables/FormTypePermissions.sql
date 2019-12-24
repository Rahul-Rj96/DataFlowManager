CREATE TABLE [dbo].[FormTypePermissions] (
    [FormTypePermissionId] INT IDENTITY (1, 1) NOT NULL,
    [PermissionId]         INT NOT NULL,
    [FormTypeId]           INT NOT NULL,
    [RoleId]               INT NOT NULL,
    CONSTRAINT [PK_Constraint_FormTypePermissionId] PRIMARY KEY CLUSTERED ([FormTypePermissionId] ASC),
    CONSTRAINT [FK_Constraint_FormTypePermissions_FormTypeId] FOREIGN KEY ([FormTypeId]) REFERENCES [dbo].[FormType] ([FormTypeId]),
    CONSTRAINT [FK_Constraint_FormTypePermissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId]),
    CONSTRAINT [FK_Constraint_FormTypePermissions_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);

