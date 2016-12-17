CREATE PROCEDURE [dbo].[ReservaCartaDePorte]
(
	@UsuarioReserva nvarchar(100),
	@IdEstablecimientoOrigen int,
	@IdTipoCartaDePorte int,
	@IdGrupoEmpresa int,
	@IdEmpresa int
)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @cartaID int
	DECLARE @cartaDePorte varchar(50)
	DECLARE @cee varchar(50)
	DECLARE @asocia int

	-- Chequeo si el establecimiento debe tener o no cartas de porte asignadas
	SELECT @asocia = AsociaCartaDePorte from establecimiento where idestablecimiento = @IdEstablecimientoOrigen

	IF @asocia = 0
	BEGIN

		SELECT	top 1 @cartaID = CartasDePorte.IdCartaDePorte , @cartaDePorte = CartasDePorte.NumeroCartaDePorte, @cee = CartasDePorte.NumeroCee
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is null
		order by LoteCartasDePorte.FechaVencimiento,CartasDePorte.NumeroCartaDePorte

	END
	ELSE
	BEGIN

		SELECT	top 1 @cartaID = CartasDePorte.IdCartaDePorte , @cartaDePorte = CartasDePorte.NumeroCartaDePorte, @cee = CartasDePorte.NumeroCee
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is not null
		and		LoteCartasDePorte.EstablecimientoOrigen = @IdEstablecimientoOrigen
		order by LoteCartasDePorte.FechaVencimiento,CartasDePorte.NumeroCartaDePorte

	END


	IF @cartaDePorte is null
	BEGIN
		select 0
	END
	ELSE
	BEGIN

		UPDATE	CartasDePorte 
		SET		Estado = 1,
				FechaReserva = getdate(),
				UsuarioReserva = @UsuarioReserva
		WHERE	IdGrupoEmpresa = @IdGrupoEmpresa 
				and IdCartaDePorte = @cartaID


		INSERT INTO Solicitudes 
			(IdEstablecimientoProcedencia, ObservacionAfip,NumeroCartaDePorte,	Cee, FechaCreacion,	UsuarioCreacion,IdTipoDeCarta, IdEmpresa) 
		VALUES 
			(@IdEstablecimientoOrigen, 'Reserva de Carta de Porte', @cartaDePorte , @cee,getDate(),@UsuarioReserva,@IdTipoCartaDePorte, @IdEmpresa)

		select @cartaID

	END



END