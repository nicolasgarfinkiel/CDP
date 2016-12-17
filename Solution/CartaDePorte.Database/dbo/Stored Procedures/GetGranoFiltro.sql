CREATE PROCEDURE [dbo].[GetGranoFiltro] 
(
	@texto nvarchar(500),
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @descripcion varchar(500)
	DECLARE @material varchar(500)
	DECLARE @cosecha varchar(500)
	DECLARE @especie varchar(500)

	SET @columnList = 'g.*'
	SET @descripcion = '''%' + @texto + '%'''
	SET @material = '''%' + @texto + '%'''
	SET @cosecha = '''%' + @texto + '%'''
	SET @especie = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from Grano g, vEspecie e, vCosecha c where  g.IdGrupoEmpresa = ' + CAST(@IdGrupoEmpresa AS VARCHAR) + ' and g.Activo = 1  and	g.IdEspecieAfip = e.IdEspecie and g.IdCosechaAfip = c.IdCosecha and (g.Descripcion like ' + @descripcion + ' or g.IdMaterialSap like ' + @material + ' or e.Descripcion like ' + @especie + ' or c.Descripcion like ' + @cosecha + ')'


	EXEC (@sqlCommand)

END

