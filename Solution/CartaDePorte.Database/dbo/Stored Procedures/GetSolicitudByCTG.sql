CREATE PROCEDURE [dbo].[GetSolicitudByCTG]
(
	@NumeroCTG nvarchar(40),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Solicitudes 
	WHERE 
		IdEmpresa = @IdEmpresa
		and Ctg = @NumeroCTG
	
END
