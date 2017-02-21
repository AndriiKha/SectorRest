CREATE TABLE [dbo].[OrdersProducts] (
    [ORD_PR_RECID]     INT IDENTITY (1, 1) NOT NULL,
    [ORD_PR_IDProduct] INT NOT NULL,
    [ORD_PR_IDOrder]   INT NOT NULL,
    [ORD_PR_Count]     INT NULL,
    PRIMARY KEY CLUSTERED ([ORD_PR_RECID] ASC),
    FOREIGN KEY ([ORD_PR_IDProduct]) REFERENCES [dbo].[Products] ([PR_RECID]),
    FOREIGN KEY ([ORD_PR_IDOrder]) REFERENCES [dbo].[Orders] ([ORD_RECID])
);

