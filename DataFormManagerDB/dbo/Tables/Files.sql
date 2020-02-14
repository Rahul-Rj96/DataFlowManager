CREATE TABLE [dbo].[Files] (
    [FileId]       INT             IDENTITY (1, 1) NOT NULL,
    [FileRecord]   VARBINARY (MAX) NOT NULL,
    [FileType]     VARCHAR (500)   NULL,
    [FileFullName] VARCHAR (500)   NULL,
    [FileFormType] VARCHAR (500)   NULL,
    CONSTRAINT [PK_Constraint_FileId] PRIMARY KEY CLUSTERED ([FileId] ASC)
);





