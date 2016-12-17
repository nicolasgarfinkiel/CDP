create PROCEDURE [dbo].[GetPartidoByFiltro]
(
      @filtro varchar(1000)
)
AS
BEGIN

      SET NOCOUNT ON;

            SELECT top 20 l.*,p.Descripcion NombreProvincia FROM Partido l, provincia p
            where l.idprovincia = p.codigo
            and         l.descripcion like '%' + @filtro + '%'

      
END
