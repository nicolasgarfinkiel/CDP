

--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetCliente')
	DROP PROCEDURE [GetCliente]
GO
CREATE PROCEDURE [dbo].[GetCliente]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN


		if (@IdEmpresa = 0)
		begin

			SELECT * 
			FROM Cliente 
			where IdCliente not in (2000151,3000352)

		end
		else
		begin

			SELECT * 
			FROM 
				Cliente 
			where 
				IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
				and IdCliente not in (2000151,3000352)

		end			
	END
	ELSE
	BEGIN
		SELECT * FROM Cliente WHERE IdCliente = @Id
	END


END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorBySAPID')
	DROP PROCEDURE [GetProveedorBySAPID]
GO
CREATE PROCEDURE [dbo].[GetProveedorBySAPID]
(
	@Id nvarchar(50)
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM 
		Proveedor 
	WHERE 
		Sap_Id = @Id


END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarCliente')
	DROP PROCEDURE [GuardarCliente]
GO
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


	IF EXISTS(select IdCliente from Cliente WHERE	IdCliente = @IdCliente)
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
				EsProspecto = @EsProspecto,
				IdSapOrganizacionDeVenta = @IdSapOrganizacionDeVenta
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
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarProveedor')
	DROP PROCEDURE [GuardarProveedor]
GO
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
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarUpdateClienteProspecto')
	DROP PROCEDURE [GuardarUpdateClienteProspecto]
GO
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
GO









--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetChoferFiltro')
    DROP PROCEDURE [GetChoferFiltro]
GO
CREATE PROCEDURE [dbo].[GetChoferFiltro] 
(
    @texto nvarchar(500),
    @IdGrupoEmpresa int
)
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @sqlCommand varchar(1000)
    DECLARE @columnList varchar(75)
    DECLARE @nombre varchar(100)
    DECLARE @apellido varchar(100)
    DECLARE @cuit varchar(100)
    SET @columnList = '*'
    SET @nombre = '''%' + @texto + '%'''
    SET @apellido = '''%' + @texto + '%'''
    SET @cuit = '''%' + @texto + '%'''

    SET @sqlCommand = 'SELECT ' + @columnList + ' FROM Chofer WHERE IdGrupoEmpresa = ' + CAST(@IdGrupoEmpresa AS VARCHAR) + ' and Activo = 1 and (Nombre like ' + @nombre + ' or Apellido like ' + @apellido + ' or Cuit like ' + @cuit + ')'

    EXEC (@sqlCommand)

END


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetChoferUsadas')
    DROP PROCEDURE [GetChoferUsadas]
GO
CREATE PROCEDURE [dbo].[GetChoferUsadas]
(
    @IdGrupoEmpresa int
)
AS
BEGIN
    select    Chofer.* 
    from     (select top 4 IdChofer,count(*) cnt from solicitudes group by IdChofer order by count(*) desc) tabla,
            Chofer
    where    tabla.IdChofer = Chofer.IdChofer
            and Chofer.IdGrupoEmpresa = @IdGrupoEmpresa

--select * from solicitudes

END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetChoferByCuit')
    DROP PROCEDURE [GetChoferByCuit]
GO
CREATE PROCEDURE [dbo].[GetChoferByCuit]
(
    @Cuit nvarchar(50),
    @IdGrupoEmpresa int
)
AS
BEGIN

    SET NOCOUNT ON;
    SELECT * FROM chofer WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Cuit = @Cuit


END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetChofer')
    DROP PROCEDURE [GetChofer]
GO
CREATE PROCEDURE [dbo].[GetChofer]
(
    @Id int,
    @IdGrupoEmpresa int
)
AS
BEGIN

    SET NOCOUNT ON;

    IF @Id = 0
    BEGIN
        SELECT * FROM Chofer WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Activo = 1
    END
    ELSE
    BEGIN
        SELECT * FROM Chofer WHERE IdChofer = @Id
    END


END

--------------------------
----------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarChofer')
    DROP PROCEDURE [GuardarChofer]
GO
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

        UPDATE    Chofer 
        SET        Nombre = @Nombre,
                Apellido = @Apellido,
                Cuit = @Cuit,
                Camion = @Camion,
                Acoplado = @Acoplado,
                FechaModificacion = getDate(),
                UsuarioModificacion = @Usuario,
                EsChoferTransportista = @EsChoferTransportista
        WHERE    IdChofer = @IdChofer

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
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'EliminarChofer')
    DROP PROCEDURE [EliminarChofer]
GO
CREATE PROCEDURE [dbo].[EliminarChofer]
(
    @IdChofer int,
    @Usuario varchar(200)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE    Chofer 
    SET        Activo = 0,
            FechaModificacion = getDate(),
            UsuarioModificacion = @Usuario
    WHERE    IdChofer = @IdChofer

END
GO

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------