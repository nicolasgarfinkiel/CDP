CREATE PROCEDURE [dbo].[GetC1116AByFiltro] 
(
	@texto nvarchar(500)
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
					' from	C1116A ' +
					' where (NroCertificadoc1116a like ' + @Valor + 
					' or	UsuarioCreacion like ' + @Valor + ') order by 1 desc'

	

	--PRINT @sqlCommand
	EXEC (@sqlCommand)

END
GO

