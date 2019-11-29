CREATE TABLE [dbo].[UserForms] (
    [UserId]     INT NOT NULL,
    [FormId]     INT NOT NULL,
    [UserFormId] INT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Constraint_UserFormId] PRIMARY KEY CLUSTERED ([UserFormId] ASC),
    CONSTRAINT [FK_Constraint_FormId] FOREIGN KEY ([FormId]) REFERENCES [dbo].[Form] ([FormId]),
    CONSTRAINT [FK_Constraint_UserId2] FOREIGN KEY ([UserId]) REFERENCES [dbo].[RCKRUser] ([UserId])
);

