CREATE PROCEDURE [dbo].[GuardarCosecha]
(
	@IdCosecha int,
	@Codigo nvarchar(100),
	@Descripcion nvarchar(300),
	@IdGrupoEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;

	IF @IdCosecha > 0
	BEGIN

		UPDATE	Cosecha 
		SET		Codigo = @Codigo,
				Descripcion = @Descripcion
		WHERE	IdCosecha = @IdCosecha

	END
	ELSE
	BEGIN

		INSERT INTO Cosecha 
			(Codigo,Descripcion,IdGrupoEmpresa) 
		VALUES 
			(@Codigo,@Descripcion,@IdGrupoEmpresa)

		SELECT SCOPE_IDENTITY()

	END

END

