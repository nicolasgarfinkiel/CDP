CREATE PROCEDURE [dbo].[GetChoferByCuit]
(
	@Cuit nvarchar(50),
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM chofer WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Cuit = @Cuit


END