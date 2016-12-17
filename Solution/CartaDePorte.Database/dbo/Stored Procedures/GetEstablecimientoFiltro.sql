
-- [dbo].[GetEstablecimientoFiltro] 'k'
CREATE PROCEDURE [dbo].[GetEstablecimientoFiltro] 
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @descripcion varchar(500)

	SET @columnList = '*'
	SET @descripcion = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from Establecimiento  WHERE IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and Activo = 1 and (Descripcion like ' + @descripcion + ')'

	EXEC (@sqlCommand)

END


--select * from Establecimiento
