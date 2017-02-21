CREATE TABLE [dbo].[Products] (
    [PR_RECID]         INT           IDENTITY (1, 1) NOT NULL,
    [PR_Name]          NVARCHAR (50) NOT NULL,
    [PR_IDImage]       INT           NULL,
    [PR_IDCategory]    INT           NOT NULL,
    [PR_IDTab]         INT           NOT NULL,
    [PR_IDIngredients] INT           NULL,
    [PR_Price]         MONEY         NOT NULL,
    PRIMARY KEY CLUSTERED ([PR_RECID] ASC),
    FOREIGN KEY ([PR_IDImage]) REFERENCES [dbo].[Images] ([IM_RECID]),
    FOREIGN KEY ([PR_IDCategory]) REFERENCES [dbo].[Category] ([CT_RECID]),
    FOREIGN KEY ([PR_IDTab]) REFERENCES [dbo].[Tabs] ([TB_RECID]),
    FOREIGN KEY ([PR_IDIngredients]) REFERENCES [dbo].[Ingredients] ([IG_RECID])
);

