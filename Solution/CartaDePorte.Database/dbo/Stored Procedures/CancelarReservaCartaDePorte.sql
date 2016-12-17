CREATE PROCEDURE [dbo].[CancelarReservaCartaDePorte] 
(
	@nroCartaDePorte varchar(50),
	@UsuarioReserva nvarchar(100),
	@IdGrupoEmpresa int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	UPDATE	CartasDePorte
	SET		Estado = 0,
			FechaReserva = null,
			UsuarioReserva = null
	WHERE	IdGrupoEmpresa = @IdGrupoEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte

	DELETE FROM Solicitudes 
	WHERE	IdEmpresa = @IdEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte

	SELECT @nroCartaDePorte

				
END
