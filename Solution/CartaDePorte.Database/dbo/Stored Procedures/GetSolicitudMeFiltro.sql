CREATE PROCEDURE [dbo].[GetSolicitudMeFiltro]
(
	@texto nvarchar(500),
	@estadoAFIP nvarchar(100),
	@estadoSAP nvarchar(100)
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = 's.*,e.Descripcion DescripcionEstablecimientoOrigen,tc.Descripcion as DescripcionTipoDeCarta,p2.Nombre as ProveedorNombreTitularCartaDePorte, s.IdEmpresa'
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT top 100000 ' + @columnList + 
					' from	solicitudes s INNER JOIN establecimiento e ON s.IdEstablecimientoProcedencia = e.IdEstablecimiento	' + 
					' INNER JOIN TipoDeCarta tc ON tc.IdTipoDeCarta = s.IdTipoDeCarta ' +	
					' LEFT OUTER JOIN Proveedor p ON p.IdProveedor = s.IdEstablecimientoProcedencia ' +
					' LEFT OUTER JOIN Proveedor p2 ON p2.IdProveedor = s.idProveedorTitularCartaDePorte ' +
					' where (s.NumeroCartaDePorte like ' + @Valor + 
					' or		s.Ctg like ' + @Valor + 
					' or		s.ObservacionAfip like ' + @Valor + 
					' or		e.Descripcion like ' + @Valor + 
					' or		s.UsuarioCreacion like ' + @Valor + ') '

	IF @estadoAFIP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnAFIP in (' + @estadoAFIP + ')'
	END

	IF @estadoSAP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnSAP in (' + @estadoSAP + ')'
	END

	SET @sqlCommand = @sqlCommand + ' order by 1 desc'

PRINT @sqlCommand

	EXEC (@sqlCommand)

END

