CREATE PROCEDURE [dbo].[GetLoteCartasDePorte]
AS
BEGIN
	SET NOCOUNT ON;
	
	select * from LoteCartasDePorte order by FechaVencimiento asc

END