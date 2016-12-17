CREATE PROCEDURE [dbo].[GetSolicitudRecibidaFiltro]
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = '* '
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT top 10000 ' + @columnList + 
					' from	SolicitudesRecibidas ' +
					' where IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and (NumeroCartaDePorte like ' + @Valor + 
					' or	Ctg like ' + @Valor + 
					' or	UsuarioCreacion like ' + @Valor + ') order by 1 desc'

	

	--PRINT @sqlCommand
	EXEC (@sqlCommand)

END

