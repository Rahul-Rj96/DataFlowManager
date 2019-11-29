CREATE TABLE [dbo].[FormType] (
    [FormTypeId] INT            NOT NULL,
    [FormName]   VARCHAR (255)  NOT NULL,
    [FormConfig] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Constraint_FormTypeId] PRIMARY KEY CLUSTERED ([FormTypeId] ASC)
);

