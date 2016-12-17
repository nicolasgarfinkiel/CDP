CREATE PROCEDURE [dbo].[AnularReservaCartaDePorte] 
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
	SET		FechaReserva = null,
			UsuarioReserva = null
	WHERE	IdGrupoEmpresa = @IdGrupoEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte

	UPDATE	Solicitudes 
	SET		EstadoEnSAP = 4,
			EstadoEnAFIP = 3,
			ObservacionAfip = 'Reserva de Carta de Porte ANULADA'
	WHERE	IdEmpresa = @IdEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte


	SELECT @nroCartaDePorte

				
END
