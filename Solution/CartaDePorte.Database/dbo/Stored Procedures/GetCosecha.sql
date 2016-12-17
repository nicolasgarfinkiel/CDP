CREATE PROCEDURE [dbo].[GetCosecha]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Cosecha WHERE IdGrupoEmpresa = @IdGrupoEmpresa order by descripcion
	END
	ELSE
	BEGIN
		SELECT * FROM Cosecha WHERE IdCosecha = @Id
	END


END
