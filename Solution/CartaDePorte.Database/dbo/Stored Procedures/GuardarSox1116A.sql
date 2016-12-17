CREATE PROCEDURE [dbo].[GuardarSox1116A]
(
	@IdCartaDePorte1116A int,
	@IdSolicitud int,
	@Numero1116A varchar(100),
	@Fecha1116A datetime,
	@Usuario varchar(150)
)

AS
BEGIN
	SET NOCOUNT ON;

	IF @IdCartaDePorte1116A > 0
	BEGIN

		UPDATE	CartaDePorte1116A
		SET		IdSolicitud = @IdSolicitud,
				Numero1116A = @Numero1116A,
				Fecha1116A = @Fecha1116A,
				FechaModificacion = getDate(),
				UsuarioModificacion = @Usuario
		WHERE	IdCartaDePorte1116A = @IdCartaDePorte1116A

	END
	ELSE
	BEGIN

		INSERT INTO CartaDePorte1116A (IdSolicitud,Numero1116A,Fecha1116A,FechaCreacion,UsuarioCreacion) VALUES 
		(@IdSolicitud,@Numero1116A,@Fecha1116A,getDate(),@Usuario)

	END

END