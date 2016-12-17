--create PROCEDURE [dbo].[GetLocalidadByFiltro]
--(
--      @filtro varchar(1000)
--)
--AS
--BEGIN

--      SET NOCOUNT ON;

--            SELECT top 20 l.*,p.Descripcion NombreProvincia FROM Localidad l, provincia p
--            where l.idprovincia = p.codigo
--            and         l.descripcion like '%' + @filtro + '%'

      
--END


CREATE PROCEDURE [dbo].[GetLocalidadByText] 
(
      @filtro varchar(1000)
)
AS
BEGIN

            SET NOCOUNT ON;

            SELECT      top 20 l.*,p.Descripcion NombreProvincia  
            FROM        Localidad l, provincia p     
            where       l.idprovincia = p.codigo  
            and         l.descripcion  + ' (' + P.Descripcion + ')' = '' + @filtro + ''

      
END
