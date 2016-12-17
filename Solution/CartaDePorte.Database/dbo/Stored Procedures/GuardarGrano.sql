CREATE PROCEDURE [dbo].[GuardarGrano]
(
	@IdGrano int,
	@Descripcion varchar(500),
	@IdMaterialSap varchar(100),
	@EspecieAfip int,
	@CosechaAfip int,
	@IdTipoGrano int,
	@SujetoALote varchar(100),
	@Usuario varchar(150),
	@IdGrupoEmpresa int
)

AS
BEGIN
	SET NOCOUNT ON;

	IF @IdTipoGrano = 0
	BEGIN
		SET @IdTipoGrano = null
	END

	IF @IdGrano > 0
	BEGIN

		UPDATE	Grano
		SET		Descripcion = @Descripcion,
				IdMaterialSap = @IdMaterialSap,
				IdEspecieAfip = @EspecieAfip,
				IdCosechaAfip = @CosechaAfip,
				SujetoALote = @SujetoALote,
				IdTipoGrano = @IdTipoGrano,
				FechaModificacion = getDate(),
				UsuarioModificacion = @Usuario
		WHERE	IdGrano = @IdGrano


	END
	ELSE
	BEGIN

		INSERT INTO Grano 
			(Descripcion,IdMaterialSap,IdEspecieAfip,IdCosechaAfip,IdTipoGrano,SujetoALote,FechaCreacion,UsuarioCreacion,IdGrupoEmpresa) 
		VALUES 
			(@Descripcion,@IdMaterialSap,@EspecieAfip,@CosechaAfip,@IdTipoGrano,@SujetoALote,getDate(),@Usuario,@IdGrupoEmpresa)

	END

END

