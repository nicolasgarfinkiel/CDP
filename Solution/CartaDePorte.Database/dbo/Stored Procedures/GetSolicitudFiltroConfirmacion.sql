CREATE PROCEDURE [dbo].[GetSolicitudFiltroConfirmacion] 
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @Valor varchar(500)

	SET @columnList = 's.*,e.Descripcion DescripcionEstablecimientoOrigen'
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'select	TOP 100 s.*,es.Descripcion DescripcionEstablecimientoOrigen from solicitudes s, establecimiento es where  s.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and s.IdEstablecimientoDestino = es.IdEstablecimiento and s.EstadoEnAFIP = 9 ' + 
			' UNION ALL ' +
			' select	TOP 100 s.*,es.Descripcion DescripcionEstablecimientoOrigen' +
			' from	solicitudes s, Empresa e, cliente c,establecimiento es ' +
			' where	s.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) +
			' and       s.EstadoEnAFIP = 1 ' +
			' and		s.IdEstablecimientoDestino = es.IdEstablecimiento ' +
			' and		c.IdCliente = e.IdCliente ' +
			' and		es.IdInterlocutorDestinatario = c.IdCliente ' +
			' and		s.ObservacionAfip <> ''CTG Otorgado Carga Masiva'' ' +
			' AND ( es.Descripcion like ' + @Valor + 
				' or		e.Descripcion like ' + @Valor + 
				' or		s.UsuarioCreacion like ' + @Valor + ') '



	SET @sqlCommand = @sqlCommand + ' order by 1 asc'

	EXEC (@sqlCommand)

END
