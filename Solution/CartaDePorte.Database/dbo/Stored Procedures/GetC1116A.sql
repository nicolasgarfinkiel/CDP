﻿CREATE PROCEDURE [dbo].[GetC1116A]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = 0
	BEGIN
		SELECT * FROM C1116A ORDER BY 1 desc
	END
	ELSE
	BEGIN
		SELECT * FROM C1116A WHERE IdC1116A = @Id
	END
	
END
GO