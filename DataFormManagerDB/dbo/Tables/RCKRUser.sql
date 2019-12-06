CREATE TABLE [dbo].[RCKRUser] (
    [UserId]   INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (255) NOT NULL,
    [EmailId]  VARCHAR (255) NOT NULL,
    [Sub]      VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Constraint_UserId] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [UK_Constraint_EmailId] UNIQUE NONCLUSTERED ([EmailId] ASC)
);



