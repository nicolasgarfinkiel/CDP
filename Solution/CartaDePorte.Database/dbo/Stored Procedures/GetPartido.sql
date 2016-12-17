CREATE PROCEDURE [dbo].[GetPartido]
(
      @Id int
)
AS
BEGIN

      SET NOCOUNT ON;
      IF @Id = 0
      BEGIN
            SELECT l.*,p.Descripcion NombreProvincia FROM Partido l, provincia p
            where l.idprovincia = p.codigo
      END
      ELSE
      BEGIN
            SELECT l.*,p.Descripcion NombreProvincia FROM Partido l, provincia p
            where l.idprovincia = p.codigo
            and l.Codigo = @Id
      END
      
END



SET ANSI_NULLS ON
