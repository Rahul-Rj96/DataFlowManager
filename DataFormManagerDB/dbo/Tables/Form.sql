CREATE TABLE [dbo].[Form] (
    [FormId]     INT            IDENTITY (1, 1) NOT NULL,
    [FormTypeId] INT            NOT NULL,
    [FormData]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Constraint_FormId] PRIMARY KEY CLUSTERED ([FormId] ASC),
    CONSTRAINT [FK_Constraint_FormTypeId] FOREIGN KEY ([FormTypeId]) REFERENCES [dbo].[FormType] ([FormTypeId])
);



