CREATE PROCEDURE [dbo].[EliminarEstablecimiento]
(
	@IdEstablecimiento int,
	@Usuario varchar(150)
)

AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	Establecimiento
	SET		Activo = 0,
			FechaModificacion = getDate(),
			UsuarioModificacion = @Usuario
	WHERE	IdEstablecimiento = @IdEstablecimiento

END

