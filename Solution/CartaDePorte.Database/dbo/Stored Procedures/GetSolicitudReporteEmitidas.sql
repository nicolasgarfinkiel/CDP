CREATE PROCEDURE [dbo].[GetSolicitudReporteEmitidas]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT	* 
	FROM	Solicitudes 
	where	
			IdEmpresa = @IdEmpresa
	and		FechaDeEmision between @FechaDesde and @FechaHasta
	and		IdEstablecimientoProcedencia in (select IdEstablecimiento from establecimiento where asociacartadeporte = 1)
	
END

