
-- [dbo].[GetEmpresaFiltro]  'k'
CREATE PROCEDURE [dbo].[GetEmpresaFiltro] 
(
	@texto nvarchar(500)
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @Descripcion varchar(500)
	DECLARE @Cuit varchar(500)
	DECLARE @RazonSocial varchar(500)
	DECLARE @IdCliente varchar(500)

	SET @columnList = 'e.*,c.RazonSocial,c.Cuit'
	SET @Descripcion = '''%' + @texto + '%'''
	SET @Cuit = '''%' + @texto + '%'''
	SET @RazonSocial = '''%' + @texto + '%'''
	SET @IdCliente = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from	Empresa e,		Cliente c where	e.IdCliente = c.IdCliente and		( Descripcion like ' + @Descripcion + ' or		c.Cuit like ' + @Cuit + '  or		c.RazonSocial like ' + @RazonSocial + '  or		c.IdCliente like ' + @IdCliente + ')'


	EXEC (@sqlCommand)

END
