
CREATE PROCEDURE [dbo].[GuardarUpdateClienteProspecto]
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
	@idclienteprospecto numeric,
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


	IF @IDTipoDocumentoSAP = 0
	BEGIN
		SET @IDTipoDocumentoSAP = null;
	END
	IF @IdClientePrincipal = 0
	BEGIN
		SET @IdClientePrincipal = null;
	END

	UPDATE	Cliente 
	SET		IdCliente = @IdCliente,
			RazonSocial = @RazonSocial,
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
			EsProspecto = @EsProspecto,
			IdSapOrganizacionDeVenta = @IdSapOrganizacionDeVenta
	WHERE	Cuit = @Cuit and EsProspecto = 1

	IF @idclienteprospecto > 0
	BEGIN
		-- @IdCliente @idclienteprospecto
		UPDATE Solicitudes SET IdClienteIntermediario = @IdCliente WHERE IdClienteIntermediario = @idclienteprospecto
		UPDATE Solicitudes SET IdClienteRemitenteComercial = @IdCliente WHERE  IdClienteRemitenteComercial = @idclienteprospecto
		UPDATE Solicitudes SET IdClienteCorredor = @IdCliente WHERE IdClienteCorredor = @idclienteprospecto
		UPDATE Solicitudes SET IdClienteEntregador = @IdCliente WHERE IdClienteEntregador = @idclienteprospecto
		UPDATE Solicitudes SET IdClienteDestinatario = @IdCliente WHERE  IdClienteDestinatario = @idclienteprospecto
		UPDATE Solicitudes SET IdClienteDestino = @IdCliente WHERE IdClienteDestino = @idclienteprospecto
		UPDATE Solicitudes SET IdClientePagadorDelFlete = @IdCliente WHERE IdClientePagadorDelFlete = @idclienteprospecto
		UPDATE Solicitudes SET IdClienteDestinatarioCambio = @IdCliente WHERE IdClienteDestinatarioCambio = @idclienteprospecto

		-- Chequeo el estado en SAP
		-- Si es estado 9 (EnEsperaPorProspecto) lo paso a 0 (Pendiente) para que se envie a SAP
		DECLARE @estadoensap int

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClienteIntermediario = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClienteIntermediario = @idclienteprospecto
		END

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClienteRemitenteComercial = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClienteRemitenteComercial = @idclienteprospecto
		END

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClienteCorredor = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClienteCorredor = @idclienteprospecto
		END

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClienteEntregador = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClienteEntregador = @idclienteprospecto
		END

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClienteDestinatario = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClienteDestinatario = @idclienteprospecto
		END

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClienteDestino = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClienteDestino = @idclienteprospecto
		END


		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClientePagadorDelFlete = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClientePagadorDelFlete = @idclienteprospecto
		END

		SELECT @estadoensap = EstadoEnSAP from solicitudes WHERE IdClienteDestinatarioCambio = @idclienteprospecto
		IF @estadoensap = 9
		BEGIN
			UPDATE Solicitudes SET EstadoEnSAP = 0 WHERE IdClienteDestinatarioCambio = @idclienteprospecto
		END

	END

END



