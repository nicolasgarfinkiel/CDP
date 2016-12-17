CREATE PROCEDURE [dbo].[GuardarC1116ADetalle]
(
	@Idc1116aDetalle int,
	@Idc1116a int,
	@NumeroCartaDePorte int,
	@NumeroCertificadoAsociado int,
	@KgBrutos decimal(18,2),
	@FechaRemesa datetime
)

AS
BEGIN




	SET NOCOUNT ON;

	IF @Idc1116aDetalle > 0
	BEGIN

		UPDATE	c1116aDetalle
		SET		Idc1116a	=	@Idc1116a,
				NumeroCartaDePorte	=	@NumeroCartaDePorte,
				NumeroCertificadoAsociado	=	@NumeroCertificadoAsociado,
				KgBrutos	=	@KgBrutos,
				FechaRemesa = @FechaRemesa
		WHERE	Idc1116aDetalle	=	@Idc1116aDetalle

		SELECT @Idc1116aDetalle

	END
	ELSE
	BEGIN

		INSERT INTO c1116aDetalle (Idc1116a,NumeroCartaDePorte,NumeroCertificadoAsociado,KgBrutos,FechaRemesa) VALUES 
					(@Idc1116a,@NumeroCartaDePorte,@NumeroCertificadoAsociado,@KgBrutos,@FechaRemesa)
		
		DECLARE @scope int
		select @scope = SCOPE_IDENTITY()

		SELECT @scope
	END


END
GO
