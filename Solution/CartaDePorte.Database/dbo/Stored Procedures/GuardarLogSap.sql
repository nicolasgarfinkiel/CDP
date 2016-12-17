CREATE PROCEDURE [dbo].[GuardarLogSap]
(
    @IDoc varchar(50),
    @Origen varchar(50),
    @NroDocumentoRE varchar(50),
    @NroDocumentoSap varchar(50),
    @TipoMensaje varchar(50),
    @TextoMensaje varchar(500),
	@NroEnvio int
)
AS
BEGIN
	SET NOCOUNT ON;


	INSERT INTO LogSap (IDoc, Origen, NroDocumentoRE, NroDocumentoSap, TipoMensaje, TextoMensaje,NroEnvio) VALUES 
	(@IDoc, @Origen, @NroDocumentoRE, @NroDocumentoSap, @TipoMensaje, @TextoMensaje,@NroEnvio)


END
