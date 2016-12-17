CREATE PROCEDURE [dbo].[GetEmpresaAdmin]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT 
			e.*, ge.IdApp, ge.IdPais
		FROM 
			Empresa e
			INNER JOIN GrupoEmpresa ge
				ON e.IdGrupoEmpresa = ge.IdGrupoEmpresa
	END
	ELSE
	BEGIN
		SELECT 
			e.*, ge.IdApp, ge.IdPais
		FROM 
			Empresa e
			INNER JOIN GrupoEmpresa ge
				ON e.IdGrupoEmpresa = ge.IdGrupoEmpresa
		WHERE 
			IdEmpresa = @Id
	END


END