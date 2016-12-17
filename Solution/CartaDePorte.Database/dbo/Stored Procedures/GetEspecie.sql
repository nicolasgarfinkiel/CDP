CREATE PROCEDURE [dbo].[GetEspecie]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Especie WHERE IdGrupoEmpresa = @IdGrupoEmpresa order by descripcion
	END
	ELSE
	BEGIN
		SELECT * FROM Especie WHERE IdEspecie = @Id
	END


END
