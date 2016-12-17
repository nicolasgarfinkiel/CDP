CREATE PROCEDURE [dbo].[GetOneByCodigo]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * FROM Especie WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Codigo = @Id


END
