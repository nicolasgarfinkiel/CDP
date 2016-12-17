CREATE PROCEDURE [dbo].[GuardarEstadoCartaDePorte]
(
	@Id int,
	@estado int,
	@IdGrupoEmpresa int
)

AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	cartasdeporte
	SET		Estado = @estado
	WHERE	IdCartaDePorte = @Id 
			and IdGrupoEmpresa = @IdGrupoEmpresa

END
