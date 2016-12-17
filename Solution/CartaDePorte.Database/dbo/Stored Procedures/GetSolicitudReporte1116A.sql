CREATE PROCEDURE [dbo].[GetSolicitudReporte1116A]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@ModoFecha int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	-- Si el modo es 1 el rango de fechas es por FechaEmision
	-- Si el modo es 2 el rango de fechas es por Fecha1116A

	IF @ModoFecha = 1
	BEGIN
		SELECT	sol.*,sox.Numero1116A, sox.Fecha1116A 
		FROM	Solicitudes sol, CartaDePorte1116A sox
		where	
				sol.IdEmpresa = @IdEmpresa
		and		sol.FechaDeEmision between @FechaDesde and @FechaHasta
		and		sol.IdSolicitud = sox.IdSolicitud
		and		sox.Activo = 1
	END
	BEGIN
		SELECT	sol.*,sox.Numero1116A, sox.Fecha1116A 
		FROM	Solicitudes sol, CartaDePorte1116A sox
		where	
				sol.IdEmpresa = @IdEmpresa
		and		sox.Fecha1116A between @FechaDesde and @FechaHasta
		and		sol.IdSolicitud = sox.IdSolicitud
		and		sox.Activo = 1
	END
	


END
