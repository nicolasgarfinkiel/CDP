CREATE PROCEDURE [dbo].[GetGrano]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Grano WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Activo = 1
	END
	ELSE
	BEGIN
		SELECT * FROM Grano WHERE IdGrano = @Id
	END


END
