CREATE PROCEDURE [dbo].[Delete]
	@ItemId int
AS
	DELETE tblStore
	WHERE itemCode = @ItemId
