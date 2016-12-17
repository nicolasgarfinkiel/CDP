CREATE PROCEDURE [dbo].[GuardarChofer]
(
	@IdChofer int,
	@Nombre varchar(300),
	@Apellido varchar(300),
	@Cuit varchar(100),
	@Camion nvarchar(50),
	@Acoplado nvarchar(50),
	@Usuario varchar(200),
	@EsChoferTransportista bit,
	@IdGrupoEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;


	IF @IdChofer > 0
	BEGIN

		UPDATE	Chofer 
		SET		Nombre = @Nombre,
				Apellido = @Apellido,
				Cuit = @Cuit,
				Camion = @Camion,
				Acoplado = @Acoplado,
				FechaModificacion = getDate(),
				UsuarioModificacion = @Usuario,
				EsChoferTransportista = @EsChoferTransportista
		WHERE	IdChofer = @IdChofer

		SELECT @IdChofer

	END
	ELSE
	BEGIN

		INSERT INTO Chofer 
		(Nombre,Apellido,Cuit,Camion,Acoplado,FechaCreacion,UsuarioCreacion,EsChoferTransportista,IdGrupoEmpresa) 
		VALUES 
		(@Nombre,@Apellido,@Cuit,@Camion,@Acoplado,getDate(),@Usuario,@EsChoferTransportista,@IdGrupoEmpresa)

		SELECT SCOPE_IDENTITY()
	END

END