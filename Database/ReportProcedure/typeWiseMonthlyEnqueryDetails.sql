--- Drop the Procedure at First If Exists
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[reportTypeWiseMOnthlyEnqueryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[reportTypeWiseMOnthlyEnqueryDetails]
GO

--- Create the Procedure
CREATE PROCEDURE [dbo].[reportTypeWiseMOnthlyEnqueryDetails]
	@CreatedYear INT
	, @CreatedMonth INT
AS
BEGIN	
	SELECT A.CreatedMonth
		, A.CreatedYear
		, A.EnqueryType
		, SUM(A.Outstanding) + SUM(A.Quoted) + SUM(A.Closed) AS [TotalSubmitted]
		, SUM(A.Outstanding) AS [Outstanding]
		, SUM(A.Quoted) AS [Quoted]
		, SUM(A.Closed) AS [Closed] 
	FROM 
	(
		SELECT MONTH(e.CreatedOn) AS [CreatedMonth]
			, YEAR(e.CreatedOn) AS [CreatedYear]
			, et.[Name] AS [EnqueryType]
			, CASE WHEN e.StatusID=1 THEN 1 ELSE 0 END [Outstanding]
			, CASE WHEN e.StatusID=2 THEN 1 ELSE 0 END [Quoted]
			, CASE WHEN e.StatusID=3 THEN 1 ELSE 0 END [Closed]  
		FROM Enquiries e
		INNER JOIN EnquiryTypes et ON et.ID = e.TypeID
	) A
	WHERE A.CreatedMonth = @CreatedMonth
		AND A.CreatedYear = @CreatedYear
	GROUP BY A.CreatedMonth
		, A.CreatedYear
		, A.EnqueryType
	ORDER BY A.EnqueryType
END
