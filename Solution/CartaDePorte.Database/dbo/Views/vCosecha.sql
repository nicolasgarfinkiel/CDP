CREATE VIEW [dbo].[vCosecha]
AS
SELECT        
	IdCosecha, Codigo, Descripcion, FechaCreacion, IdGrupoEmpresa
FROM
	dbo.Cosecha

UNION 
	SELECT 0,0,'',GETDATE(),0