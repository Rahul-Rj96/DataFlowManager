CREATE TABLE [dbo].[RCKRUser] (
    [UserId]   INT           NOT NULL,
    [Username] VARCHAR (255) NOT NULL,
    [Password] VARCHAR (255) NOT NULL,
    [EmailId]  VARCHAR (255) NOT NULL,
    [PhoneNo]  VARCHAR (15)  NOT NULL,
    CONSTRAINT [PK_Constraint_UserId] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [UK_Constraint_EmailId] UNIQUE NONCLUSTERED ([EmailId] ASC),
    CONSTRAINT [UK_Constraint_PhoneNo] UNIQUE NONCLUSTERED ([PhoneNo] ASC)
);

