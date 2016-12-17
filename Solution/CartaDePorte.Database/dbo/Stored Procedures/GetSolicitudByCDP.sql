CREATE PROCEDURE [dbo].[GetSolicitudByCDP]
(
	@NumeroCartaDePorte nvarchar(40)
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Solicitudes 
	WHERE 
		NumeroCartaDePorte = @NumeroCartaDePorte
	
END
