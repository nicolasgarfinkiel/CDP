CREATE PROCEDURE [dbo].[GuardarEstablecimiento]
(
	@IdEstablecimiento int,
	@Descripcion varchar(500),
	@Direccion varchar(500),
	@Localidad int,
	@Provincia int,
	@IDAlmacenSAP varchar(100),
	@IDCentroSAP varchar(100),
	@IdInterlocutorDestinatario int,
	@RecorridoEstablecimiento int,
	@cebe nvarchar(50),
	@expedicion nvarchar(50),
	@establecimientoAfip nvarchar(50),
	@asociaCartaDePorte bit,
	@Usuario varchar(150),
	@IdEmpresa int
)

AS
BEGIN
	SET NOCOUNT ON;


	IF @IdEstablecimiento > 0
	BEGIN

		UPDATE	Establecimiento
		SET		Descripcion = @Descripcion,
				Direccion = @Direccion,
				Localidad = @Localidad,
				Provincia = @Provincia,
				IDAlmacenSAP = @IDAlmacenSAP,
				IDCentroSAP = @IDCentroSAP,
				IdInterlocutorDestinatario = @IdInterlocutorDestinatario,
				RecorridoEstablecimiento = @RecorridoEstablecimiento,
				IDCEBE = @cebe,
				idexpedicion = @expedicion,
				EstablecimientoAfip = @establecimientoAfip,
				AsociaCartaDePorte = @asociaCartaDePorte,
				FechaModificacion = getDate(),
				UsuarioModificacion = @Usuario
		WHERE	IdEstablecimiento = @IdEstablecimiento


	END
	ELSE
	BEGIN

		INSERT INTO Establecimiento 
			(Descripcion,Direccion,Localidad,Provincia,IDAlmacenSAP,IDCentroSAP,IdInterlocutorDestinatario,RecorridoEstablecimiento,idcebe,idexpedicion,EstablecimientoAfip,AsociaCartaDePorte,FechaCreacion,UsuarioCreacion,IdEmpresa) 
		VALUES 		
			(@Descripcion,@Direccion,@Localidad,@Provincia,@IDAlmacenSAP,@IDCentroSAP,@IdInterlocutorDestinatario,@RecorridoEstablecimiento,@cebe,@expedicion,@establecimientoAfip,@asociaCartaDePorte,getDate(),@Usuario,@IdEmpresa)

	END

END


/*

Establecimiento

IdEstablecimiento,Descripcion,Direccion,Localidad,Provincia,IDAlmacenSAP,IDCentroSAP,IdInterlocutorDestinatario,RecorridoEstablecimiento,FechaCreacion,UsuarioCreacion,FechaModificacion,UsuarioModificacion
*/
