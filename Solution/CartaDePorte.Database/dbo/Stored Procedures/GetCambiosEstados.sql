--[dbo].[GetCambiosEstados] '540733695'
CREATE PROCEDURE [dbo].[GetCambiosEstados]
(
	@cdp nvarchar(50)
)
AS
BEGIN

	SET NOCOUNT ON;
	select * from logsolicitudes where numerocartadeporte = @cdp order by fechamodificacion 

END
