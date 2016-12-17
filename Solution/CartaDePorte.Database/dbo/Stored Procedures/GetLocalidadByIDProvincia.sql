CREATE PROCEDURE [dbo].[GetLocalidadByIDProvincia] 
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;
	--SELECT * FROM Localidad WHERE IdProvincia = @Id ORDER BY Descripcion
		SELECT l.*,p.Descripcion NombreProvincia FROM Localidad l, provincia p
		where	l.idprovincia = p.codigo
		and		l.IdProvincia = @Id

	
END
