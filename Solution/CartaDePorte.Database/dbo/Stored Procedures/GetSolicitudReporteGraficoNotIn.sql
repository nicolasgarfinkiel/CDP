CREATE PROCEDURE [dbo].[GetSolicitudReporteGraficoNotIn]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	
	select Convert(varchar,FechaDeEmision,111) Fecha,numerocartadeporte,Ctg CodigoAfip, CodigoRespuestaEnvioSAP CodigoSAP,EstadoEnAFIP,EstadoEnSAP 
	from 
		solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and ctg <> '' 
		and ctg is not null 
		and EstadoEnAFIP in (1,4,5,6,8,9) 
		and numerocartadeporte not in 
			(select numerocartadeporte from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnSAP in (2)) 
		and FechaDeEmision between @FechaDesde and @FechaHasta
	
	UNION ALL
	
	select Convert(varchar,FechaDeEmision,111) Fecha, numerocartadeporte,Ctg CodigoAfip, CodigoRespuestaEnvioSAP CodigoSAP,EstadoEnAFIP,EstadoEnSAP 
	from 
		solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and ctg <> '' 
		and ctg is not null 
		and EstadoEnSAP in (2) 
		and numerocartadeporte not in 
			(select numerocartadeporte from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnAFIP in (1,4,5,6,8,9)) 
		and FechaDeEmision between @FechaDesde and @FechaHasta
	
	
	order by 
		fecha,numerocartadeporte

END

