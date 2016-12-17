-- [dbo].[GetLogSapUltimoNroEnvio] '528795587'
CREATE PROCEDURE [dbo].[GetLogSapUltimoNroEnvio]
(
	@cartaDePorte varchar(50)
)
AS
BEGIN

	DECLARE @cnt int

	SET NOCOUNT ON;
	SELECT @cnt = max(NroEnvio) FROM LogSap WHERE NroDocumentoRE = @cartaDePorte
	IF @cnt is NULL
	BEGIN
		select 0
	END
	ELSE
	BEGIN
		select @cnt
	END

END

