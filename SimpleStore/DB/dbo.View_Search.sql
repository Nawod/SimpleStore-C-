CREATE PROCEDURE [dbo].[View&Search]
	@itemName nvarchar(50)
AS
	SELECT *
	FROM tblStore
	WHERE itemName = @itemName OR itemCd = @itemName OR itemBrand = @itemName