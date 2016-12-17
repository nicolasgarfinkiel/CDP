CREATE PROCEDURE [dbo].[GetCartaDePorteDisponible]
(
	@idEstablecimiento int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @idEstablecimiento = 0
	BEGIN

		SELECT	top 1 CartasDePorte.*
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is null
		order by LoteCartasDePorte.FechaVencimiento,  CAST(CartasDePorte.NumeroCartaDePorte AS INT) 

	END
	ELSE
	BEGIN

		SELECT	top 1 CartasDePorte.*
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is not null
		and		LoteCartasDePorte.EstablecimientoOrigen = @idEstablecimiento
		order by LoteCartasDePorte.FechaVencimiento,  CAST(CartasDePorte.NumeroCartaDePorte AS INT) 


	END


END
