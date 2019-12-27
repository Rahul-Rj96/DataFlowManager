CREATE TABLE [dbo].[FormType] (
    [FormTypeId]     INT            IDENTITY (1, 1) NOT NULL,
    [FormName]       VARCHAR (255)  NOT NULL,
    [FormConfig]     NVARCHAR (MAX) NOT NULL,
    [IsSelfAssigned] INT            NULL,
    CONSTRAINT [PK_Constraint_FormTypeId] PRIMARY KEY CLUSTERED ([FormTypeId] ASC)
);





