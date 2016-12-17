CREATE PROCEDURE [dbo].[GuardarUpdateProveedorProspecto]
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
	@idproveedorprospecto numeric,
	@IdEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;

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
			EsProspecto = @EsProspecto
	WHERE	NumeroDocumento = @NumeroDocumento and EsProspecto = 1

	IF @idproveedorprospecto > 0
	BEGIN
		-- @IdCliente @idclienteprospecto
		UPDATE Solicitudes SET idProveedorTitularCartaDePorte = @IdProveedor WHERE idProveedorTitularCartaDePorte = @IdProveedor
		UPDATE Solicitudes SET IdProveedorTransportista = @IdProveedor WHERE  IdProveedorTransportista = @IdProveedor
		
		-- Chequeo el estado en SAP
		-- Si es estado 9 (EnEsperaPorProspecto) lo paso a 0 (Pendiente) para que se envie a SAP
		DECLARE @estadoensap int

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE idProveedorTitularCartaDePorte = @IdProveedor 
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE idProveedorTitularCartaDePorte = @IdProveedor
		END

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdProveedorTransportista = @IdProveedor 
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdProveedorTransportista = @IdProveedor
		END


	END


END
