CREATE PROCEDURE [dbo].[GetClienteFiltro] 
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

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from	cliente where IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ')  and ( IdCliente like ' + @Valor + ' or		RazonSocial like ' + @Valor + '  or		Cuit like ' + @Valor + ') and IdCliente not in (2000151,3000352) '


	EXEC (@sqlCommand)

END


