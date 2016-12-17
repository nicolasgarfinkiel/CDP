
CREATE PROCEDURE [dbo].[GetUtilizadoPorLote]
(
	@idLote int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT count(*) FROM CartasDePorte WHERE IdLoteLoteCartasDePorte = @idLote and Estado <> 0

END