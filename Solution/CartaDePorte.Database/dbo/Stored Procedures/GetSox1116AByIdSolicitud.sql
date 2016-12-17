CREATE PROCEDURE [dbo].[GetSox1116AByIdSolicitud]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = 0
	BEGIN
		SELECT * FROM CartaDePorte1116A WHERE Activo = 1 ORDER BY 1 desc
	END
	ELSE
	BEGIN
		SELECT * FROM CartaDePorte1116A WHERE IdSolicitud = @Id and Activo = 1
	END
	
END