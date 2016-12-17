CREATE PROCEDURE [dbo].[GuardarProveedor]
(
	@IdProveedor int,
	@Sap_Id nvarchar(100),
	@Nombre nvarchar(100),
	@IdTipoDocumentoSAP int,
	@NumeroDocumento nvarchar(100),
	@Calle nvarchar(400),
	@Piso nvarchar(100), 
	@Departamento nvarchar(100),
	@Numero nvarchar(100),
	@CP nvarchar(100),
	@Ciudad nvarchar(100),
	@Pais nvarchar(100),
	@Activo bit,
	@EsProspecto bit,
	@IdSapOrganizacionDeVenta int,
	@IdEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;

	IF (@IdSapOrganizacionDeVenta = NULL OR @IdSapOrganizacionDeVenta = 0)
	BEGIN
		select @IdSapOrganizacionDeVenta = e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa;
	END


	IF @IdProveedor > 0
	BEGIN

		UPDATE	Proveedor 
		SET		Sap_Id = @Sap_Id,
				Nombre = @Nombre,
				IdTipoDocumentoSAP = @IdTipoDocumentoSAP,
				NumeroDocumento = @NumeroDocumento,
				Calle = @Calle,
				Piso = @Piso,
				Departamento = @Departamento,
				Numero = @Numero,
				CP = @CP,
				Ciudad = @Ciudad,
				Pais = @Pais,
				Activo = @Activo,
				EsProspecto = @EsProspecto,
				IdSapOrganizacionDeVenta = @IdSapOrganizacionDeVenta
		WHERE	IdProveedor = @IdProveedor


	END
	ELSE
	BEGIN


		INSERT INTO Proveedor 
			(Sap_Id,Nombre,IdTipoDocumentoSAP,NumeroDocumento,Calle,Piso,Departamento,Numero,CP,Ciudad,Pais,Activo,EsProspecto, IdSapOrganizacionDeVenta) 
		VALUES 
			(@Sap_Id,@Nombre,@IdTipoDocumentoSAP,@NumeroDocumento,@Calle,@Piso,@Departamento,@Numero,@CP,@Ciudad,@Pais,@Activo,@EsProspecto,@IdSapOrganizacionDeVenta)

	END

END
