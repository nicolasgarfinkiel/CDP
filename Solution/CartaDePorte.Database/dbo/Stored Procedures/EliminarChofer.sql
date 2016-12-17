CREATE PROCEDURE [dbo].[EliminarChofer]
(
	@IdChofer int,
	@Usuario varchar(200)
)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	Chofer 
	SET		Activo = 0,
			FechaModificacion = getDate(),
			UsuarioModificacion = @Usuario
	WHERE	IdChofer = @IdChofer

END
