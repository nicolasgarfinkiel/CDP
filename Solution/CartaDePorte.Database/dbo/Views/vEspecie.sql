
CREATE VIEW [dbo].[vEspecie]
AS
SELECT        
	IdEspecie, Codigo, Descripcion, FechaCreacion, IdGrupoEmpresa
FROM
	dbo.Especie

UNION 
	SELECT 0,0,'',GETDATE(),0