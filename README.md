# 資料庫  
Member: 使用者帳戶  
    CREATE TABLE [dbo].[Member] (  
    [MUid]    NVARCHAR (50) NOT NULL,  
    [MPwd]    NVARCHAR (50) NULL,  
    [MName]   NVARCHAR (50) NULL,  
    [MMail]   NVARCHAR (50) NULL,  
    [MRole]   NVARCHAR (50) NULL,  
    [MStatus] INT           DEFAULT ((1)) NULL,  
    PRIMARY KEY CLUSTERED ([MUid] ASC)  
);  
-----
