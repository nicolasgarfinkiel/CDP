
-- [dbo].[GetChoferFiltro] ''
CREATE PROCEDURE [dbo].[GetLoteCartasDePorteFiltro] 
(
	@loteDesde int,
	@tieneDisponible int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand varchar(1000)
	

	SET @sqlCommand = '';
	SET @sqlCommand = @sqlCommand + ' SELECT lcdp.*'
	SET @sqlCommand = @sqlCommand + ' FROM LoteCartasDePorte lcdp';
	SET @sqlCommand = @sqlCommand + ' WHERE 1=1 ';
	
	IF (@loteDesde > 0)
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND lcdp.Desde >= ' + CONVERT(varchar,@loteDesde) ;
	END

	IF (@tieneDisponible = 1)
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND lcdp.IdLoteCartasDePorte IN (SELECT TOP 1 cdp.IdLoteLoteCartasDePorte FROM CartasDePorte cdp WHERE cdp.Estado = 0 AND cdp.IdLoteLoteCartasDePorte = lcdp.IdLoteCartasDePorte)';
	END
	
	SET @sqlCommand = @sqlCommand + ' ORDER BY  lcdp.Desde';

	EXEC (@sqlCommand)

END
