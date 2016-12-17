CREATE PROCEDURE [dbo].[GetTipoDocumentoSAP]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM TipoDocumentoSAP
	END
	ELSE
	BEGIN
		SELECT * FROM TipoDocumentoSAP WHERE SAP_Id = @Id
	END


END
