CREATE PROCEDURE [dbo].[GuardarCliente]
(
	@IdCliente int,
	@RazonSocial varchar(300),
	@NombreFantasia varchar(300),
	@Cuit varchar(100),
	@IDTipoDocumentoSAP int = null,
	@IdClientePrincipal int = null,
	@Calle varchar(100),
	@Numero varchar(100),
	@Dto varchar(100),
	@Piso varchar(100),
	@Cp varchar(100),
	@Poblacion varchar(100),
	@Activo bit,
	@GrupoComercial varchar(100),
	@ClaveGrupo varchar(100),
	@Tratamiento varchar(100),
	@DescripcionGe varchar(100),
	@EsProspecto bit,
	@IdSapOrganizacionDeVenta int,
	@IdEmpresa int	
)
AS
BEGIN
	SET NOCOUNT ON;


	IF (@IdSapOrganizacionDeVenta IS NULL OR @IdSapOrganizacionDeVenta = 0 AND @IdEmpresa > 0)
	BEGIN
		select @IdSapOrganizacionDeVenta = e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa;
	END


	IF @IDTipoDocumentoSAP = 0
	BEGIN
		SET @IDTipoDocumentoSAP = null;
	END
	IF @IdClientePrincipal = 0
	BEGIN
		SET @IdClientePrincipal = null;
	END


	IF EXISTS(select IdCliente from Cliente WHERE IdCliente = @IdCliente and IdSapOrganizacionDeVenta = @IdSapOrganizacionDeVenta)
	BEGIN

		UPDATE	Cliente 
		SET		RazonSocial = @RazonSocial,
				NombreFantasia = @NombreFantasia,
				Cuit = @Cuit,
				IDTipoDocumentoSAP = @IDTipoDocumentoSAP,
				IdClientePrincipal = @IdClientePrincipal,
				Calle = @Calle,
				Numero = @Numero,
				Dto = @Dto,
				Piso = @Piso,
				Cp = @Cp,
				Poblacion = @Poblacion,
				Activo = @Activo,
				GrupoComercial = @GrupoComercial,
				ClaveGrupo = @ClaveGrupo,
				Tratamiento = @Tratamiento,
				DescripcionGe = @DescripcionGe,
				EsProspecto = @EsProspecto
		WHERE	IdCliente = @IdCliente


	END
	ELSE
	BEGIN


		INSERT INTO Cliente 
		(IdCliente,RazonSocial,NombreFantasia,Cuit,IdTipoDocumentoSAP,IdClientePrincipal,Calle,Numero,Dto,Piso,Cp,Poblacion,Activo,GrupoComercial,ClaveGrupo,Tratamiento,DescripcionGe,EsProspecto,IdSapOrganizacionDeVenta) 
		VALUES 
		(@IdCliente, @RazonSocial,@NombreFantasia,@Cuit,@IdTipoDocumentoSAP,@IdClientePrincipal,@Calle,@Numero,@Dto,@Piso,@Cp,@Poblacion,1,@GrupoComercial,@ClaveGrupo,@Tratamiento,@DescripcionGe,@EsProspecto,@IdSapOrganizacionDeVenta)

	END

END
