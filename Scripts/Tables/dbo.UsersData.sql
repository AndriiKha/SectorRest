CREATE TABLE [dbo].[UsersData] (
    [USR_RECID]    INT            IDENTITY (1, 1) NOT NULL,
    [USR_Login]    NVARCHAR (50)  NOT NULL,
    [USR_Password] NVARCHAR (50)  NOT NULL,
    [USR_FName]    NVARCHAR (MAX) NOT NULL,
    [USR_LName]    NVARCHAR (MAX) NOT NULL,
    [USR_Email]    NVARCHAR (MAX) NULL,
    [USR_Role]     NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_UsersData] PRIMARY KEY CLUSTERED ([USR_RECID] ASC)
);

