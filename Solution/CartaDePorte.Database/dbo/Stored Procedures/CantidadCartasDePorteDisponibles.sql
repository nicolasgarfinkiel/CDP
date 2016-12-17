CREATE PROCEDURE [dbo].[CantidadCartasDePorteDisponibles]
(
	@IdGrupoEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;
	select	count(*) cnt 
	from	CartasDePorte, LoteCartasDePorte
	where	
			CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
	and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
	and		CartasDePorte.Estado = 0 
	and		LoteCartasDePorte.FechaVencimiento >= getdate()
	and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte

END
