CREATE PROCEDURE [dbo].[GetSolicitudTop] 
(
	@Top int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)

	SET @columnList = '*'
	SET @sqlCommand = 'SELECT top ' + CONVERT(varchar,@Top) + ' ' + @columnList + ' from	Solicitudes where s.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' order by 1 desc'

	EXEC (@sqlCommand)	


END
