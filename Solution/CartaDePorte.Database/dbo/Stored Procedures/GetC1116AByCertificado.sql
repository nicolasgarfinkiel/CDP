﻿CREATE PROCEDURE [dbo].[GetC1116AByCertificado]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM C1116A WHERE NroCertificadoc1116a = @Id

	
END
GO