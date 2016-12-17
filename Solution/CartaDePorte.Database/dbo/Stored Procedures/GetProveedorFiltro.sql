CREATE PROCEDURE [dbo].[GetProveedorFiltro] 
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

	SET @columnList = '*'
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from	proveedor where	IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ') and ( Sap_id like ' + @Valor + ' or		Nombre like ' + @Valor + '  or		NumeroDocumento like ' + @Valor + ')'


	EXEC (@sqlCommand)

END

