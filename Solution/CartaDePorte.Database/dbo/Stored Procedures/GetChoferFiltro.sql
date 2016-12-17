
-- [dbo].[GetChoferFiltro] ''
CREATE PROCEDURE [dbo].[GetChoferFiltro] 
(
	@texto nvarchar(500),
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand varchar(1000)
	DECLARE @columnList varchar(75)
	DECLARE @nombre varchar(100)
	DECLARE @apellido varchar(100)
	DECLARE @cuit varchar(100)
	SET @columnList = '*'
	SET @nombre = '''%' + @texto + '%'''
	SET @apellido = '''%' + @texto + '%'''
	SET @cuit = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT ' + @columnList + ' FROM Chofer WHERE IdGrupoEmpresa = ' + CAST(@IdGrupoEmpresa AS VARCHAR) + ' and Activo = 1 and (Nombre like ' + @nombre + ' or Apellido like ' + @apellido + ' or Cuit like ' + @cuit + ')'

	EXEC (@sqlCommand)

END
