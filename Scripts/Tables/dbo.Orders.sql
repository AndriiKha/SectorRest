CREATE TABLE [dbo].[Orders] (
    [ORD_RECID]     INT      IDENTITY (1, 1) NOT NULL,
    [ORD_UserID]    INT      NOT NULL,
    [ORD_OrderDate] DATETIME NOT NULL,
    [ORD_PriceCost] MONEY    NOT NULL,
    [ORD_GetMoney]  MONEY    NOT NULL,
    PRIMARY KEY CLUSTERED ([ORD_RECID] ASC),
    FOREIGN KEY ([ORD_UserID]) REFERENCES [dbo].[UsersData] ([USR_RECID])
);

