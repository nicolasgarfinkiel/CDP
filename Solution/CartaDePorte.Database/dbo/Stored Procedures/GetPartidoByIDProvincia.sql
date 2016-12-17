CREATE PROCEDURE [dbo].[GetPartidoByIDProvincia] 
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;
	--SELECT * FROM Partido WHERE IdProvincia = @Id ORDER BY Descripcion
		SELECT l.*,p.Descripcion NombreProvincia FROM Partido l, provincia p
		where	l.idprovincia = p.codigo
		and		l.IdProvincia = @Id

	
END
