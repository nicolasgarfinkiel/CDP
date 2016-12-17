CREATE PROCEDURE [dbo].[Eliminar1116A]
(
	@Id int,
	@Usuario varchar(200)
)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	CartaDePorte1116A
	SET		Activo = 0,
			FechaModificacion = getDate(),
			UsuarioModificacion = @Usuario
	WHERE	IdCartaDePorte1116A = @Id

END