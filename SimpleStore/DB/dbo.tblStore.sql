CREATE TABLE [dbo].[tblStore]
(
	[itemCode] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[itemCd] INT NULL,
    [itemName] NVARCHAR(50) NULL, 
    [itemBrand] NVARCHAR(50) NULL, 
    [RDate] DATE NULL
)
