CREATE PROCEDURE [dbo].[GetLogSap]
(
	@cartaDePorte varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM LogSap WHERE NroDocumentoRE = @cartaDePorte

END