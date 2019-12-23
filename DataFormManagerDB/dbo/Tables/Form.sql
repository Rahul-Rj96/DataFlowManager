CREATE TABLE [dbo].[Form] (
    [FormId]          INT            IDENTITY (1, 1) NOT NULL,
    [FormTypeId]      INT            NOT NULL,
    [CreatedDateTime] DATETIME       CONSTRAINT [DF_Form_CreatedDateTime] DEFAULT (getdate()) NOT NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [CreatedBy]       INT            NOT NULL,
    [FormData]        NVARCHAR (MAX) NULL,
    [IsDeleted]       INT            CONSTRAINT [DF_Form_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Constraint_FormId] PRIMARY KEY CLUSTERED ([FormId] ASC),
    CONSTRAINT [FK_Constraint_FormTypeId] FOREIGN KEY ([FormTypeId]) REFERENCES [dbo].[FormType] ([FormTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Form_RCKRUserId] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[RCKRUser] ([UserId])
);







