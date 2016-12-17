CREATE PROCEDURE [dbo].[GetLoteCartasDePorteReporte] 
(
	@FechaDesde datetime,
	@FechaHasta datetime
)
AS
BEGIN
	SET NOCOUNT ON;
	
	select * from 
		LoteCartasDePorte 
	where 
		fechacreacion between @FechaDesde and @FechaHasta 
	order by 
		FechaVencimiento asc

END
