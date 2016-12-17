CREATE PROCEDURE [dbo].[GetSolicitudReporteGrafico]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	select afip.Fecha,ISNULL(afip.cntAfip,0) cntAfip,ISNULL(sap.cntSap,0) cntSap 
	from 
		(select Convert(varchar,FechaDeEmision,111) Fecha, count(*) cntAfip from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnAFIP in (1,4,5,6,8,9) group by Convert(varchar,FechaDeEmision,111)) afip full 
		outer join	(select Convert(varchar,FechaDeEmision,111) Fecha, count(*) cntSap from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnSAP in (2) group by Convert(varchar,FechaDeEmision,111)) sap 
			ON afip.Fecha = sap.Fecha
	WHERE 
		afip.Fecha between @FechaDesde and @FechaHasta
	order by 
		Fecha 	

END
