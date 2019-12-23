CREATE TABLE [dbo].[Role] (
    [RoleId]   INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] VARCHAR (255) NULL,
    CONSTRAINT [PK_Constraint_RoleId] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);



