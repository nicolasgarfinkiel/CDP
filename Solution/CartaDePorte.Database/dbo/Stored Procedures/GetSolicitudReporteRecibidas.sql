CREATE PROCEDURE [dbo].[GetSolicitudReporteRecibidas]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT	* 
	FROM	SolicitudesRecibidas 
	where	
		IdEmpresa = @IdEmpresa
		and FechaDeEmision between @FechaDesde and @FechaHasta
	order by 1 desc
	
END

