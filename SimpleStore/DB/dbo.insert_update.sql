CREATE PROCEDURE [dbo].[insert&update]
	@mode nvarchar(10),
	@itemCode int,
	@itemCd int,
	@itemName nvarchar(50),
	@itemBrand nvarchar(50),
	@RDate date,
	@Iimage image
AS
	IF @mode ='ADD'
	BEGIN
		INSERT INTO tblStore
		(
			itemCd, itemName, itemBrand, RDate , Iimage
		)
		VALUES
		(
			@itemCd, @itemName, @itemBrand, @RDate , @Iimage
		)
	END
	ELSE IF @mode = 'EDIT'
	BEGIN
		UPDATE tblStore 
		SET  itemCd = @itemCd, itemName = @itemName, itemBrand = @itemBrand, RDate = @RDate , Iimage= @Iimage
		WHERE itemCode = @itemCode
	END