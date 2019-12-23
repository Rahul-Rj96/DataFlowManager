CREATE TABLE [dbo].[UserForms] (
    [UserFormId] INT IDENTITY (1, 1) NOT NULL,
    [UserId]     INT NOT NULL,
    [FormId]     INT NOT NULL,
    CONSTRAINT [PK_Constraint_UserFormId] PRIMARY KEY CLUSTERED ([UserFormId] ASC),
    CONSTRAINT [FK_Constraint_FormId] FOREIGN KEY ([FormId]) REFERENCES [dbo].[Form] ([FormId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Constraint_UserId2] FOREIGN KEY ([UserId]) REFERENCES [dbo].[RCKRUser] ([UserId]) ON DELETE CASCADE
);



