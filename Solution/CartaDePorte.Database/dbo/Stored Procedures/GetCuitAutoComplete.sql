CREATE PROCEDURE [dbo].[GetCuitAutoComplete] 
(
	@campo nvarchar(100),
	@texto nvarchar(100),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = @campo + ' as cuit '
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT distinct top 10 ' + @columnList + 
					' from	SolicitudesRecibidas ' +
					' where IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and ' + @campo + ' like ' + @Valor 



	--PRINT @sqlCommand
	EXEC (@sqlCommand)

END



