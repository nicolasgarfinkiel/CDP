CREATE PROCEDURE [dbo].[GuardarEspecie]
(
	@IdEspecie int,
	@Codigo int,
	@Descripcion nvarchar(300),
	@IdGrupoEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;


	IF @IdEspecie > 0
	BEGIN

		UPDATE	Especie 
		SET		Codigo = @Codigo,
				Descripcion = @Descripcion
		WHERE	IdEspecie = @IdEspecie

	END
	ELSE
	BEGIN

		INSERT INTO Especie 
			(Codigo,Descripcion,IdGrupoEmpresa) 
		VALUES 
			(@Codigo,@Descripcion,@IdGrupoEmpresa)

		SELECT SCOPE_IDENTITY()

	END

END

