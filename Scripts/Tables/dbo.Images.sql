CREATE TABLE [dbo].[Images] (
    [IM_RECID] INT            IDENTITY (1, 1) NOT NULL,
    [IM_Name]  NVARCHAR (MAX) NOT NULL,
    [IM_Type]  NVARCHAR (MAX) NOT NULL,
    [IM_Byte]  VARCHAR (MAX)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IM_RECID] ASC)
);

