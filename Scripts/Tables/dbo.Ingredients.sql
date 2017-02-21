CREATE TABLE [dbo].[Ingredients] (
    [IG_RECID]       INT           IDENTITY (1, 1) NOT NULL,
    [IG_Count]       NVARCHAR (50) NOT NULL,
    [IG_Name]        NVARCHAR (50) NOT NULL,
    [IG_Description] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([IG_RECID] ASC)
);

