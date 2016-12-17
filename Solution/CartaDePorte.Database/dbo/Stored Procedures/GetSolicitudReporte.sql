CREATE PROCEDURE [dbo].[GetSolicitudReporte]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and FechaDeEmision between @FechaDesde and @FechaHasta
	
END

