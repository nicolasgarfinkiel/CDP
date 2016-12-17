CREATE PROCEDURE [dbo].[GetCartasDePorte]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * 
		FROM 
			CartasDePorte
		WHERE
			IdGrupoEmpresa = @IdGrupoEmpresa
	END
	ELSE
	BEGIN
		SELECT * 
		FROM 
			CartasDePorte 
		WHERE 
			IdGrupoEmpresa = @IdGrupoEmpresa
			and IdCartaDePorte = @Id
	END


END