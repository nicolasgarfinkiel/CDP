CREATE PROCEDURE [dbo].[EliminarGrano]
(
	@IdGrano int,
	@Usuario varchar(150)
)

AS
BEGIN

	SET NOCOUNT ON;
	UPDATE	Grano
	SET		Activo = 0,
			FechaModificacion = getDate(),
			UsuarioModificacion = @Usuario
	WHERE	IdGrano = @IdGrano


END

