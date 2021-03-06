set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[GenerateNewEnquiryNumber]
(
	@EnquiryTypeID AS INT
) RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @EnquiryNumberMax AS INT
	DECLARE @EnquiryNumberStart AS INT
	DECLARE @EnquiryNumber AS INT
	DECLARE @EnquiryNumberString AS NVARCHAR(40)
	DECLARE @CurrentYearString AS NVARCHAR(4)
	DECLARE @SuffixString AS NVARCHAR(5)
	
	
	--Owner: Rabbani
	--Date: 22-Jan-2011
	--Purpose: Start quotation number from 0001 from every new year
	
	--(This is modified code)
	SET @EnquiryNumberStart     = 1
	
	-- (This is the previous code)
	-- SET @EnquiryNumberStart     = 100 
	
	--End Rabbani
	
	SET @EnquiryNumberMax       = 9999
	
	SET @CurrentYearString      = RIGHT(CAST((YEAR(GETDATE())) AS NVARCHAR), 2)
	
	SET @EnquiryNumber          = 
		(
			SELECT ISNULL(1 + MAX(CAST(SUBSTRING([Number], 6, 4) AS INTEGER)), @EnquiryNumberStart) 
			FROM [Enquiries] 
			--Owner: Rabbani
			--Date: 22-Jan-2011
			--Purpose: Start quotation number from 0001 from every new year
			
			--New filtering option added
			WHERE SUBSTRING([Number], 4, 2) = @CurrentYearString
			
			--End Rabbani
			
			
			/* WHERE YEAR([CreatedOn]) = YEAR(GETDATE()) */ 
		)

	IF @EnquiryNumber > @EnquiryNumberMax RETURN NULL		-- CANNOT SUPPORT NUMBERS > MAX	

	SET @SuffixString           = ISNULL((SELECT TOP 1 [NumberSuffix] FROM [EnquiryTypes] WHERE [ID] = @EnquiryTypeID), '')
	
	
	SET @EnquiryNumberString    = CAST(@EnquiryNumber AS NVARCHAR)
	
	WHILE (LEN(@EnquiryNumberString) < 4)
	BEGIN
		SET @EnquiryNumberString = '0' + @EnquiryNumberString
	END
	
	
	RETURN 'OME' + @CurrentYearString + @EnquiryNumberString + @SuffixString
END


