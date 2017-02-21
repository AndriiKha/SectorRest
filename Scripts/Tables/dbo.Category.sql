CREATE TABLE [dbo].[Category] (
    [CT_RECID] INT           IDENTITY (1, 1) NOT NULL,
    [CT_IDTab] INT           NOT NULL,
    [CT_Name]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CT_RECID] ASC),
    FOREIGN KEY ([CT_IDTab]) REFERENCES [dbo].[Tabs] ([TB_RECID])
);

