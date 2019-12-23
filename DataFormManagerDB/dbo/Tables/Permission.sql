CREATE TABLE [dbo].[Permission] (
    [PermissionId]    INT           IDENTITY (1, 1) NOT NULL,
    [PermisssionName] VARCHAR (255) NULL,
    CONSTRAINT [PK_Constraint_PermissionId] PRIMARY KEY CLUSTERED ([PermissionId] ASC)
);



