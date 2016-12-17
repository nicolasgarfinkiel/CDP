CREATE PROCEDURE [dbo].[GetSolicitudTopConfirmacion] 
(
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
		
	select TOP 8 * 
	from 
		solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and EstadoEnAFIP = 9		
	
	UNION ALL

	select	TOP 8 s.*
	from	
		solicitudes s, Empresa e, cliente c,establecimiento es
	where	
			s.IdEmpresa = @IdEmpresa
	and		s.EstadoEnAFIP = 1
	and		s.IdEstablecimientoDestino = es.IdEstablecimiento
	and		c.IdCliente = e.IdCliente
	and		es.IdInterlocutorDestinatario = c.IdCliente
	and		s.ObservacionAfip <> 'CTG Otorgado Carga Masiva'


END
