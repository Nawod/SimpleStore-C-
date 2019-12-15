CREATE PROCEDURE [dbo].[View&Search]
	@itemName nvarchar(50)
AS
	SELECT *
	FROM tblStore
	WHERE itemName LIKE @itemName + '%' OR itemCd = @itemName

