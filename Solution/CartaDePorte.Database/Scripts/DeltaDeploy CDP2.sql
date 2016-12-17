------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
-- TABLAS
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Pais]
(
	[IdPais]		 INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion]    NVARCHAR (250) NULL,
	CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED ([IdPais] ASC)
)
GO

CREATE TABLE [dbo].[GrupoEmpresa]
(
	[IdGrupoEmpresa]	INT IDENTITY (1, 1) NOT NULL,
	[Descripcion]		NVARCHAR (250) NULL,
	[Activo]			BIT CONSTRAINT [DF_GrupoEmpresa_Activo] DEFAULT ((1)) NULL,
	[IdPais]			INT	NOT NULL, 
	[IdApp]				INT NOT NULL, 
	CONSTRAINT [PK_GrupoEmpresa] PRIMARY KEY CLUSTERED ([IdGrupoEmpresa] ASC)
)
GO

--CREATE TABLE [dbo].[AdminEmpresa]
--(
--	[IdEmpresa] INT            IDENTITY (1, 1) NOT NULL,
--	[Descripcion]    NVARCHAR (250) NULL,
--	[Activo]         BIT            CONSTRAINT [DF_AdminEmpresa_Activo] DEFAULT ((1)) NULL,
--	[IdGrupoEmpresa]   INT			NOT NULL, 
--	CONSTRAINT [PK_AdminEmpresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC)
--)


BEGIN TRANSACTION
GO
ALTER TABLE dbo.Empresa ADD
	IdGrupoEmpresa int NULL
GO


UPDATE dbo.Empresa
SET IdGrupoEmpresa = 1
GO

ALTER TABLE dbo.Empresa
ALTER COLUMN IdGrupoEmpresa int NOT NULL
GO

COMMIT
GO



SET IDENTITY_INSERT [dbo].[Pais] ON
INSERT INTO [dbo].[Pais] ([IdPais], [Descripcion]) VALUES (1, N'Argentina')
INSERT INTO [dbo].[Pais] ([IdPais], [Descripcion]) VALUES (2, N'Bolivia')
SET IDENTITY_INSERT [dbo].[Pais] OFF


SET IDENTITY_INSERT [dbo].[GrupoEmpresa] ON
INSERT INTO [dbo].[GrupoEmpresa] ([IdGrupoEmpresa], [Descripcion], [Activo], [IdPais], [IdApp]) VALUES (1, N'CRESUD', 1, 1, 29)
SET IDENTITY_INSERT [dbo].[GrupoEmpresa] OFF
GO


--SET IDENTITY_INSERT [dbo].[AdminEmpresa] ON
--INSERT INTO [dbo].[AdminEmpresa] ([IdEmpresa], [Descripcion], [Activo], [IdGrupoEmpresa]) VALUES (1, N'CRESUD', 1, 1)
--SET IDENTITY_INSERT [dbo].[AdminEmpresa] OFF
--GO


BEGIN TRANSACTION
GO
ALTER TABLE dbo.Establecimiento ADD
	IdEmpresa int NULL
GO


UPDATE dbo.Establecimiento
SET IdEmpresa = 22
GO

ALTER TABLE dbo.Establecimiento 
ALTER COLUMN IdEmpresa int NOT NULL
GO

COMMIT
GO





BEGIN TRANSACTION
GO
ALTER TABLE dbo.Provincia ADD
	IdPais int NULL
GO


UPDATE dbo.Provincia
SET IdPais = 1
GO

ALTER TABLE dbo.Provincia
ALTER COLUMN IdPais int NOT NULL
GO

COMMIT
GO




BEGIN TRANSACTION
GO
ALTER TABLE dbo.Chofer ADD
	IdGrupoEmpresa int NULL
GO


UPDATE dbo.Chofer
SET IdGrupoEmpresa = 1
GO

ALTER TABLE dbo.Chofer
ALTER COLUMN IdGrupoEmpresa int NOT NULL
GO

COMMIT
GO




BEGIN TRANSACTION
GO
ALTER TABLE dbo.Grano ADD
	IdGrupoEmpresa int NULL
GO


UPDATE dbo.Grano
SET IdGrupoEmpresa = 1
GO

ALTER TABLE dbo.Grano
ALTER COLUMN IdGrupoEmpresa int NOT NULL
GO

COMMIT
GO





BEGIN TRANSACTION
GO
ALTER TABLE dbo.Cosecha ADD
	IdGrupoEmpresa int NULL
GO


UPDATE dbo.Cosecha
SET IdGrupoEmpresa = 1
GO

ALTER TABLE dbo.Cosecha
ALTER COLUMN IdGrupoEmpresa int NOT NULL
GO

COMMIT
GO




BEGIN TRANSACTION
GO
ALTER TABLE dbo.Especie ADD
	IdGrupoEmpresa int NULL
GO


UPDATE dbo.Especie
SET IdGrupoEmpresa = 1
GO

ALTER TABLE dbo.Especie
ALTER COLUMN IdGrupoEmpresa int NOT NULL
GO

COMMIT
GO





BEGIN TRANSACTION
GO
ALTER TABLE dbo.Proveedor ADD
	IdSapOrganizacionDeVenta int NULL
GO


UPDATE dbo.Proveedor
SET IdSapOrganizacionDeVenta = 5000
GO

ALTER TABLE dbo.Proveedor
ALTER COLUMN IdSapOrganizacionDeVenta int NOT NULL
GO

COMMIT
GO






BEGIN TRANSACTION
GO
ALTER TABLE dbo.Cliente ADD
	IdSapOrganizacionDeVenta int NULL
GO


UPDATE dbo.Cliente
SET IdSapOrganizacionDeVenta = 5000
GO

ALTER TABLE dbo.Cliente
ALTER COLUMN IdSapOrganizacionDeVenta int NOT NULL
GO

COMMIT
GO





BEGIN TRANSACTION
GO
ALTER TABLE dbo.Solicitudes ADD
	IdEmpresa int NULL
GO


UPDATE dbo.Solicitudes
SET IdEmpresa = 22
GO

ALTER TABLE dbo.Solicitudes
ALTER COLUMN IdEmpresa int NOT NULL
GO

COMMIT
GO





BEGIN TRANSACTION
GO
ALTER TABLE dbo.SolicitudesRecibidas ADD
	IdEmpresa int NULL
GO


UPDATE dbo.SolicitudesRecibidas
SET IdEmpresa = 22
GO

ALTER TABLE dbo.SolicitudesRecibidas
ALTER COLUMN IdEmpresa int NOT NULL
GO

COMMIT
GO






BEGIN TRANSACTION
GO
ALTER TABLE dbo.logsolicitudes ADD
	IdEmpresa int NULL
GO


UPDATE dbo.logsolicitudes
SET IdEmpresa = 22
GO

ALTER TABLE dbo.logsolicitudes
ALTER COLUMN IdEmpresa int NOT NULL
GO

COMMIT
GO





BEGIN TRANSACTION
GO
ALTER TABLE dbo.LoteCartasDePorte ADD
	IdGrupoEmpresa int NULL
GO


UPDATE dbo.LoteCartasDePorte
SET IdGrupoEmpresa = 1
GO

ALTER TABLE dbo.LoteCartasDePorte
ALTER COLUMN IdGrupoEmpresa int NOT NULL
GO

COMMIT
GO





BEGIN TRANSACTION
GO
ALTER TABLE dbo.CartasDePorte ADD
	IdGrupoEmpresa int NULL
GO


UPDATE dbo.CartasDePorte
SET IdGrupoEmpresa = 1
GO

ALTER TABLE dbo.CartasDePorte
ALTER COLUMN IdGrupoEmpresa int NOT NULL
GO

COMMIT
GO



------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------









------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
-- VISTAS
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'V' AND name = 'vEspecie')
	DROP VIEW [vEspecie]
GO
CREATE VIEW [dbo].[vEspecie]
AS
SELECT        
	IdEspecie, Codigo, Descripcion, FechaCreacion, IdGrupoEmpresa
FROM
	dbo.Especie

UNION 
	SELECT 0,0,'',GETDATE(),0

GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'V' AND name = 'vCosecha')
	DROP VIEW [vCosecha]
GO
CREATE VIEW [dbo].[vCosecha]
AS
SELECT        
	IdCosecha, Codigo, Descripcion, FechaCreacion, IdGrupoEmpresa
FROM
	dbo.Cosecha

UNION 
	SELECT 0,0,'',GETDATE(),0

GO





------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------









------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
-- SP's y FN
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sysobjects WHERE name = 'ID_GRUPO_CRESUD' AND xtype IN ('FN', 'IF', 'TF'))
	DROP FUNCTION ID_GRUPO_CRESUD
GO
CREATE FUNCTION [dbo].[ID_GRUPO_CRESUD]
()
RETURNS INT
BEGIN

  RETURN 1;

END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudFiltro')
	DROP PROCEDURE [GetSolicitudFiltro]
GO
CREATE PROCEDURE [dbo].[GetSolicitudFiltro]
(
	@texto				nvarchar(500),
	@estadoAFIP			nvarchar(100),
	@estadoSAP			nvarchar(100),
	@IdGrupoEmpresa		int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = 's.*,e.Descripcion DescripcionEstablecimientoOrigen,tc.Descripcion as DescripcionTipoDeCarta,p2.Nombre as ProveedorNombreTitularCartaDePorte '
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT top 100000 ' + @columnList + 
					' from	solicitudes s ' +
					' INNER JOIN establecimiento e ON s.IdEstablecimientoProcedencia = e.IdEstablecimiento	' + 
					' INNER JOIN AdminEmpresa as ae ON e.IdEmpresa = ae.IdEmpresa and ae.IdGrupoEmpresa = ' + CAST(@IdGrupoEmpresa AS VARCHAR) + ' ' +
					' INNER JOIN TipoDeCarta tc ON tc.IdTipoDeCarta = s.IdTipoDeCarta ' +	
					' LEFT OUTER JOIN Proveedor p ON p.IdProveedor = s.IdEstablecimientoProcedencia ' +
					' LEFT OUTER JOIN Proveedor p2 ON p2.IdProveedor = s.idProveedorTitularCartaDePorte ' +
					' where (s.NumeroCartaDePorte like ' + @Valor + 
					' or		s.Ctg like ' + @Valor + 
					' or		s.ObservacionAfip like ' + @Valor + 
					' or		e.Descripcion like ' + @Valor + 
					' or		s.UsuarioCreacion like ' + @Valor + ') '

	IF @estadoAFIP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnAFIP in (' + @estadoAFIP + ')'
	END

	IF @estadoSAP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnSAP in (' + @estadoSAP + ')'
	END

	SET @sqlCommand = @sqlCommand + ' order by 1 desc'

PRINT @sqlCommand

	EXEC (@sqlCommand)

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetEstablecimiento')
	DROP PROCEDURE [GetEstablecimiento]
GO
CREATE PROCEDURE [dbo].[GetEstablecimiento]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = 0
	BEGIN
		SELECT * FROM Establecimiento WHERE IdEmpresa = @IdEmpresa and Activo = 1
	END
	ELSE
	BEGIN
		SELECT * FROM Establecimiento WHERE IdEstablecimiento = @Id
	END
	
END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetEstablecimientoFiltro')
	DROP PROCEDURE [GetEstablecimientoFiltro]
GO
CREATE PROCEDURE [dbo].[GetEstablecimientoFiltro] 
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @descripcion varchar(500)

	SET @columnList = '*'
	SET @descripcion = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from Establecimiento  WHERE IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and Activo = 1 and (Descripcion like ' + @descripcion + ')'

	EXEC (@sqlCommand)

END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetEstablecimientoOrigenDestino')
	DROP PROCEDURE [GetEstablecimientoOrigenDestino]
GO
CREATE PROCEDURE [dbo].[GetEstablecimientoOrigenDestino]
(
	@Origen int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	-- para Origen
	IF @Origen = 1
	BEGIN
		SELECT * FROM Establecimiento where IdEmpresa = @IdEmpresa and RecorridoEstablecimiento in (0,2)
	END
	ELSE
	BEGIN
	-- para Destino
		SELECT * FROM Establecimiento where IdEmpresa = @IdEmpresa and RecorridoEstablecimiento in (1,2)
	END
	
END
GO


IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarEstablecimiento')
	DROP PROCEDURE [GuardarEstablecimiento]
GO
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
				FechaModificacion = getDate()
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
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProvincia')
	DROP PROCEDURE [GetProvincia]
GO
CREATE PROCEDURE [dbo].[GetProvincia]
(
	@Id int,
	@IdPais int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = -1 --0 es codigo valido
	BEGIN
		SELECT * FROM Provincia WHERE IdPais = @IdPais ORDER BY Codigo
	END
	ELSE
	BEGIN
		SELECT * FROM Provincia WHERE Codigo = @Id
	END
	
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetGrano')
	DROP PROCEDURE [GetGrano]
GO
CREATE PROCEDURE [dbo].[GetGrano]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Grano WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Activo = 1
	END
	ELSE
	BEGIN
		SELECT * FROM Grano WHERE IdGrano = @Id
	END


END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetGranoFiltro')
	DROP PROCEDURE [GetGranoFiltro]
GO
CREATE PROCEDURE [dbo].[GetGranoFiltro] 
(
	@texto nvarchar(500),
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @descripcion varchar(500)
	DECLARE @material varchar(500)
	DECLARE @cosecha varchar(500)
	DECLARE @especie varchar(500)

	SET @columnList = 'g.*'
	SET @descripcion = '''%' + @texto + '%'''
	SET @material = '''%' + @texto + '%'''
	SET @cosecha = '''%' + @texto + '%'''
	SET @especie = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from Grano g, vEspecie e, vCosecha c where  g.IdGrupoEmpresa = ' + CAST(@IdGrupoEmpresa AS VARCHAR) + ' and g.Activo = 1  and	g.IdEspecieAfip = e.IdEspecie and g.IdCosechaAfip = c.IdCosecha and (g.Descripcion like ' + @descripcion + ' or g.IdMaterialSap like ' + @material + ' or e.Descripcion like ' + @especie + ' or c.Descripcion like ' + @cosecha + ')'


	EXEC (@sqlCommand)

END

GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarGrano')
	DROP PROCEDURE [GuardarGrano]
GO
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
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetCosecha')
	DROP PROCEDURE [GetCosecha]
GO
CREATE PROCEDURE [dbo].[GetCosecha]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Cosecha WHERE IdGrupoEmpresa = @IdGrupoEmpresa order by descripcion
	END
	ELSE
	BEGIN
		SELECT * FROM Cosecha WHERE IdCosecha = @Id
	END


END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarCosecha')
	DROP PROCEDURE [GuardarCosecha]
GO
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
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetCosechaByCodigo')
	DROP PROCEDURE [GetCosechaByCodigo]
GO
CREATE PROCEDURE [dbo].[GetCosechaByCodigo]
(
	@Id varchar(100),
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Cosecha WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Codigo = @Id

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetEspecie')
	DROP PROCEDURE [GetEspecie]
GO
CREATE PROCEDURE [dbo].[GetEspecie]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Especie WHERE IdGrupoEmpresa = @IdGrupoEmpresa order by descripcion
	END
	ELSE
	BEGIN
		SELECT * FROM Especie WHERE IdEspecie = @Id
	END


END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarEspecie')
	DROP PROCEDURE [GuardarEspecie]
GO
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
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetOneByCodigo')
	DROP PROCEDURE [GetOneByCodigo]
GO
CREATE PROCEDURE [dbo].[GetOneByCodigo]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * FROM Especie WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Codigo = @Id


END
GO








IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarUpdateProveedorProspecto')
	DROP PROCEDURE [GuardarUpdateProveedorProspecto]
GO
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
	@IdEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;

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
				EsProspecto = @EsProspecto
		WHERE	IdProveedor = @IdProveedor


	END
	ELSE
	BEGIN

		DECLARE @IdSapOrganizacionDeVenta int;
		
		select @IdSapOrganizacionDeVenta = e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa;


		INSERT INTO Proveedor 
			(Sap_Id,Nombre,IdTipoDocumentoSAP,NumeroDocumento,Calle,Piso,Departamento,Numero,CP,Ciudad,Pais,Activo,EsProspecto, IdSapOrganizacionDeVenta) 
		VALUES 
			(@Sap_Id,@Nombre,@IdTipoDocumentoSAP,@NumeroDocumento,@Calle,@Piso,@Departamento,@Numero,@CP,@Ciudad,@Pais,@Activo,@EsProspecto,@IdSapOrganizacionDeVenta)

	END

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorTransportistaUsadas')
	DROP PROCEDURE [GetProveedorTransportistaUsadas]
GO
CREATE PROCEDURE [dbo].[GetProveedorTransportistaUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Proveedor.* 
	from 	(select top 4 IdProveedorTransportista,count(*) cnt from solicitudes group by IdProveedorTransportista order by count(*) desc) tabla,
			Proveedor
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdProveedorTransportista = Proveedor.IdProveedor

--select * from solicitudes

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorTitularCartaDePorteUsadas')
	DROP PROCEDURE [GetProveedorTitularCartaDePorteUsadas]
GO
CREATE PROCEDURE [dbo].[GetProveedorTitularCartaDePorteUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Proveedor.* 
	from 	(select top 4 idProveedorTitularCartaDePorte,count(*) cnt from solicitudes group by idProveedorTitularCartaDePorte order by count(*) desc) TitularCartaDePorte,
			Proveedor
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and TitularCartaDePorte.idProveedorTitularCartaDePorte = Proveedor.IdProveedor

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorFiltro')
	DROP PROCEDURE [GetProveedorFiltro]
GO
CREATE PROCEDURE [dbo].[GetProveedorFiltro] 
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @Valor varchar(500)

	SET @columnList = '*'
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from	proveedor where	IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ') and ( Sap_id like ' + @Valor + ' or		Nombre like ' + @Valor + '  or		NumeroDocumento like ' + @Valor + ')'


	EXEC (@sqlCommand)

END
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorBySAPID')
	DROP PROCEDURE [GetProveedorBySAPID]
GO
CREATE PROCEDURE [dbo].[GetProveedorBySAPID]
(
	@Id nvarchar(50),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM 
		Proveedor 
	WHERE 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and Sap_Id = @Id


END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorByNumeroDocumento')
	DROP PROCEDURE [GetProveedorByNumeroDocumento]
GO
CREATE PROCEDURE [dbo].[GetProveedorByNumeroDocumento]
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	select * from Proveedor 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and NumeroDocumento = @texto 

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedor')
	DROP PROCEDURE [GetProveedor]
GO
CREATE PROCEDURE [dbo].[GetProveedor]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * 
		FROM 
			Proveedor 
		WHERE 
			IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
	END
	ELSE
	BEGIN
		SELECT * 
		FROM 
			Proveedor 
		WHERE 
			IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
			and IdProveedor = @Id
	END


END
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'getIdSapProveedorProspecto')
	DROP PROCEDURE [getIdSapProveedorProspecto]
GO
CREATE PROCEDURE [dbo].[getIdSapProveedorProspecto]
(
	@IdEmpresa int
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @id numeric

	select @id = Max(CONVERT(numeric,ISNULL(Sap_Id,0))) + 1 
	from 
		Proveedor 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and	esprospecto = 1

	IF @id is null or @id < 9300000000 
	BEGIN
		select 9300000000 AS ID
	END
	ELSE
	BEGIN
		select @id AS ID
	END

END
GO




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
		SELECT * 
		FROM 
			Cliente 
		where 
			IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
			and IdCliente not in (2000151,3000352)
			
	END
	ELSE
	BEGIN
		SELECT * FROM Cliente WHERE IdCliente = @Id
	END


END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteByCuit')
	DROP PROCEDURE [GetClienteByCuit]
GO
CREATE PROCEDURE [dbo].[GetClienteByCuit]
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	select * from cliente 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and cuit = @texto 

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteCorredorUsadas')
	DROP PROCEDURE [GetClienteCorredorUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteCorredorUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteCorredor,count(*) cnt from solicitudes group by IdClienteCorredor order by count(*) desc) tabla,
			Cliente
	where	
			IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteCorredor = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

--select * from solicitudes

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteDestinatarioUsadas')
	DROP PROCEDURE [GetClienteDestinatarioUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteDestinatarioUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteDestinatario,count(*) cnt from solicitudes group by IdClienteDestinatario order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteDestinatario = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

--select * from solicitudes

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteDestinoUsadas')
	DROP PROCEDURE [GetClienteDestinoUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteDestinoUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteDestino,count(*) cnt from solicitudes group by IdClienteDestino order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteDestino = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)
--select * from solicitudes

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteEntregadorUsadas')
	DROP PROCEDURE [GetClienteEntregadorUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteEntregadorUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteEntregador,count(*) cnt from solicitudes group by IdClienteEntregador order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteEntregador = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

--select * from solicitudes

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteFiltro')
	DROP PROCEDURE [GetClienteFiltro]
GO
CREATE PROCEDURE [dbo].[GetClienteFiltro] 
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @Valor varchar(500)

	SET @columnList = '*'
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = 'SELECT top 100 ' + @columnList + ' from	cliente where IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ')  and ( IdCliente like ' + @Valor + ' or		RazonSocial like ' + @Valor + '  or		Cuit like ' + @Valor + ') and IdCliente not in (2000151,3000352) '


	EXEC (@sqlCommand)

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteIntermediarioUsadas')
	DROP PROCEDURE [GetClienteIntermediarioUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteIntermediarioUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteIntermediario,count(*) cnt from solicitudes group by IdClienteIntermediario order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteIntermediario = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)
--select * from solicitudes

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClientePagadorDelFleteUsadas')
	DROP PROCEDURE [GetClientePagadorDelFleteUsadas]
GO
CREATE PROCEDURE [dbo].[GetClientePagadorDelFleteUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClientePagadorDelFlete,count(*) cnt from solicitudes group by IdClientePagadorDelFlete order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClientePagadorDelFlete = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteRemitenteComercialUsadas')
	DROP PROCEDURE [GetClienteRemitenteComercialUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteRemitenteComercialUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteRemitenteComercial,count(*) cnt from solicitudes group by IdClienteRemitenteComercial order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteRemitenteComercial = Cliente.IdCliente
		and IdCliente not in (2000151,3000352)


--select * from solicitudes

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'getIdClienteProspecto')
	DROP PROCEDURE [getIdClienteProspecto]
GO
CREATE PROCEDURE [dbo].[getIdClienteProspecto]
(
	@IdEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @id int

	select @id = Max(idCliente) + 1 
	from cliente 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and esprospecto = 1

	IF @id is null or @id  < 9300000 
	BEGIN
		select 9300000 AS ID
	END
	ELSE
	BEGIN
		select @id AS ID
	END

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
	@IdEmpresa int	
)
AS
BEGIN
	SET NOCOUNT ON;

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
				EsProspecto = @EsProspecto
		WHERE	IdCliente = @IdCliente


	END
	ELSE
	BEGIN

		DECLARE @IdSapOrganizacionDeVenta int;
		
		select @IdSapOrganizacionDeVenta = e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa;


		INSERT INTO Cliente 
		(IdCliente,RazonSocial,NombreFantasia,Cuit,IdTipoDocumentoSAP,IdClientePrincipal,Calle,Numero,Dto,Piso,Cp,Poblacion,Activo,GrupoComercial,ClaveGrupo,Tratamiento,DescripcionGe,EsProspecto,IdSapOrganizacionDeVenta) 
		VALUES 
		(@IdCliente, @RazonSocial,@NombreFantasia,@Cuit,@IdTipoDocumentoSAP,@IdClientePrincipal,@Calle,@Numero,@Dto,@Piso,@Cp,@Poblacion,1,@GrupoComercial,@ClaveGrupo,@Tratamiento,@DescripcionGe,@EsProspecto,@IdSapOrganizacionDeVenta)

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
	@IdEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;

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
			EsProspecto = @EsProspecto
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





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetMisReservas')
	DROP PROCEDURE [GetMisReservas]
GO
CREATE PROCEDURE [dbo].[GetMisReservas] 
(
	@UsuarioReserva nvarchar(100),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	IF @UsuarioReserva = ''
	BEGIN
		select	s.* 
		from	CartasDePorte c, solicitudes s	
		where	c.UsuarioReserva is not null
		and		s.ctg is null
		and		c.NumeroCartaDePorte = s.NumeroCartaDePorte
		and		s.IdEmpresa = @IdEmpresa
	END
	ELSE
	BEGIN
		select	s.* 
		from	CartasDePorte c, solicitudes s	
		where	c.UsuarioReserva = @UsuarioReserva 
		and		s.ctg is null
		and		c.NumeroCartaDePorte = s.NumeroCartaDePorte			
		and		s.IdEmpresa = @IdEmpresa
	END




END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitud')
	DROP PROCEDURE [GetSolicitud]
GO
CREATE PROCEDURE [dbo].[GetSolicitud]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = 0
	BEGIN
		SELECT * 
		FROM 
			Solicitudes 
		WHERE
			IdEmpresa = @IdEmpresa
		ORDER BY 1 desc
	END
	ELSE
	BEGIN
		SELECT * 
		FROM 
			Solicitudes 
		WHERE 
			IdSolicitud = @Id
	END
	
END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudAnulacionSAP')
	DROP PROCEDURE [GetSolicitudAnulacionSAP]
GO
CREATE PROCEDURE [dbo].[GetSolicitudAnulacionSAP]
(
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;		

	IF EXISTS (SELECT * FROM Empresa e WHERE e.IdEmpresa = @IdEmpresa AND e.IdGrupoEmpresa = [dbo].[ID_GRUPO_CRESUD]())	BEGIN

		--CRESUD
		SELECT * 
		FROM 
			Solicitudes  s
		where 
			s.IdEmpresa = @IdEmpresa
			and s.Ctg <> '' 
			and s.EstadoEnSAP = 5 
			and s.CodigoRespuestaEnvioSAP <> ''

	END
	ELSE BEGIN

		SELECT * 
		FROM 
			Solicitudes  s
		where 
			s.IdEmpresa = @IdEmpresa
			and s.EstadoEnSAP = 5 
			and s.CodigoRespuestaEnvioSAP <> ''

	END	

END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudByCDP')
	DROP PROCEDURE [GetSolicitudByCDP]
GO
CREATE PROCEDURE [dbo].[GetSolicitudByCDP]
(
	@NumeroCartaDePorte nvarchar(40),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Solicitudes 
	WHERE 
		IdEmpresa = @IdEmpresa
		and NumeroCartaDePorte = @NumeroCartaDePorte
	
END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudByCTG')
	DROP PROCEDURE [GetSolicitudByCTG]
GO
CREATE PROCEDURE [dbo].[GetSolicitudByCTG]
(
	@NumeroCTG nvarchar(40),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Solicitudes 
	WHERE 
		IdEmpresa = @IdEmpresa
		and Ctg = @NumeroCTG
	
END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudEnvioSAP')
	DROP PROCEDURE [GetSolicitudEnvioSAP]
GO
CREATE PROCEDURE [dbo].[GetSolicitudEnvioSAP]
(
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS (SELECT * FROM Empresa e WHERE e.IdEmpresa = @IdEmpresa AND e.IdGrupoEmpresa = [dbo].[ID_GRUPO_CRESUD]())	BEGIN

		--CRESUD
		SELECT * 
		FROM 
			Solicitudes s
		WHERE
			s.IdEmpresa = @IdEmpresa
			and s.Ctg <> '' 
			and s.EstadoEnAFIP <> 3 
			and s.EstadoEnSAP in (0,8)

	END
	ELSE BEGIN

		SELECT * 
		FROM 
			Solicitudes s
		WHERE
			s.IdEmpresa = @IdEmpresa
			and s.EstadoEnSAP in (0,8)

	END	
	
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudFiltro')
	DROP PROCEDURE [GetSolicitudFiltro]
GO
CREATE PROCEDURE [dbo].[GetSolicitudFiltro]
(
	@texto				nvarchar(500),
	@estadoAFIP			nvarchar(100),
	@estadoSAP			nvarchar(100),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = 's.*,e.Descripcion DescripcionEstablecimientoOrigen,tc.Descripcion as DescripcionTipoDeCarta,p2.Nombre as ProveedorNombreTitularCartaDePorte '
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT top 100000 ' + @columnList + 
					' from	solicitudes s ' +
					' INNER JOIN establecimiento e ON s.IdEstablecimientoProcedencia = e.IdEstablecimiento	' + 
					' INNER JOIN TipoDeCarta tc ON tc.IdTipoDeCarta = s.IdTipoDeCarta ' +	
					' LEFT OUTER JOIN Proveedor p ON p.IdProveedor = s.IdEstablecimientoProcedencia ' +
					' LEFT OUTER JOIN Proveedor p2 ON p2.IdProveedor = s.idProveedorTitularCartaDePorte ' +
					' where s.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and (s.NumeroCartaDePorte like ' + @Valor + 
					' or		s.Ctg like ' + @Valor + 
					' or		s.ObservacionAfip like ' + @Valor + 
					' or		e.Descripcion like ' + @Valor + 
					' or		s.UsuarioCreacion like ' + @Valor + ') '

	IF @estadoAFIP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnAFIP in (' + @estadoAFIP + ')'
	END

	IF @estadoSAP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnSAP in (' + @estadoSAP + ')'
	END

	SET @sqlCommand = @sqlCommand + ' order by 1 desc'

PRINT @sqlCommand

	EXEC (@sqlCommand)

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudFiltroConfirmacion')
	DROP PROCEDURE [GetSolicitudFiltroConfirmacion]
GO
CREATE PROCEDURE [dbo].[GetSolicitudFiltroConfirmacion] 
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)
	DECLARE @Valor varchar(500)

	SET @columnList = 's.*,e.Descripcion DescripcionEstablecimientoOrigen'
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'select	TOP 100 s.*,es.Descripcion DescripcionEstablecimientoOrigen from solicitudes s, establecimiento es where  s.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and s.IdEstablecimientoDestino = es.IdEstablecimiento and s.EstadoEnAFIP = 9 ' + 
			' UNION ALL ' +
			' select	TOP 100 s.*,es.Descripcion DescripcionEstablecimientoOrigen' +
			' from	solicitudes s, Empresa e, cliente c,establecimiento es ' +
			' where	s.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) +
			' and       s.EstadoEnAFIP = 1 ' +
			' and		s.IdEstablecimientoDestino = es.IdEstablecimiento ' +
			' and		c.IdCliente = e.IdCliente ' +
			' and		es.IdInterlocutorDestinatario = c.IdCliente ' +
			' and		s.ObservacionAfip <> ''CTG Otorgado Carga Masiva'' ' +
			' AND ( es.Descripcion like ' + @Valor + 
				' or		e.Descripcion like ' + @Valor + 
				' or		s.UsuarioCreacion like ' + @Valor + ') '



	SET @sqlCommand = @sqlCommand + ' order by 1 asc'

	EXEC (@sqlCommand)

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudReporte')
	DROP PROCEDURE [GetSolicitudReporte]
GO
CREATE PROCEDURE [dbo].[GetSolicitudReporte]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and FechaDeEmision between @FechaDesde and @FechaHasta
	
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudReporte1116A')
	DROP PROCEDURE [GetSolicitudReporte1116A]
GO
CREATE PROCEDURE [dbo].[GetSolicitudReporte1116A]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@ModoFecha int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	-- Si el modo es 1 el rango de fechas es por FechaEmision
	-- Si el modo es 2 el rango de fechas es por Fecha1116A

	IF @ModoFecha = 1
	BEGIN
		SELECT	sol.*,sox.Numero1116A, sox.Fecha1116A 
		FROM	Solicitudes sol, CartaDePorte1116A sox
		where	
				sol.IdEmpresa = @IdEmpresa
		and		sol.FechaDeEmision between @FechaDesde and @FechaHasta
		and		sol.IdSolicitud = sox.IdSolicitud
		and		sox.Activo = 1
	END
	BEGIN
		SELECT	sol.*,sox.Numero1116A, sox.Fecha1116A 
		FROM	Solicitudes sol, CartaDePorte1116A sox
		where	
				sol.IdEmpresa = @IdEmpresa
		and		sox.Fecha1116A between @FechaDesde and @FechaHasta
		and		sol.IdSolicitud = sox.IdSolicitud
		and		sox.Activo = 1
	END
	


END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudReporteEmitidas')
	DROP PROCEDURE [GetSolicitudReporteEmitidas]
GO
CREATE PROCEDURE [dbo].[GetSolicitudReporteEmitidas]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT	* 
	FROM	Solicitudes 
	where	
			IdEmpresa = @IdEmpresa
	and		FechaDeEmision between @FechaDesde and @FechaHasta
	and		IdEstablecimientoProcedencia in (select IdEstablecimiento from establecimiento where asociacartadeporte = 1)
	
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudReporteGrafico')
	DROP PROCEDURE [GetSolicitudReporteGrafico]
GO
CREATE PROCEDURE [dbo].[GetSolicitudReporteGrafico]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	select afip.Fecha,ISNULL(afip.cntAfip,0) cntAfip,ISNULL(sap.cntSap,0) cntSap 
	from 
		(select Convert(varchar,FechaDeEmision,111) Fecha, count(*) cntAfip from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnAFIP in (1,4,5,6,8,9) group by Convert(varchar,FechaDeEmision,111)) afip full 
		outer join	(select Convert(varchar,FechaDeEmision,111) Fecha, count(*) cntSap from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnSAP in (2) group by Convert(varchar,FechaDeEmision,111)) sap 
			ON afip.Fecha = sap.Fecha
	WHERE 
		afip.Fecha between @FechaDesde and @FechaHasta
	order by 
		Fecha 	

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudReporteGraficoNotIn')
	DROP PROCEDURE [GetSolicitudReporteGraficoNotIn]
GO
CREATE PROCEDURE [dbo].[GetSolicitudReporteGraficoNotIn]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	
	select Convert(varchar,FechaDeEmision,111) Fecha,numerocartadeporte,Ctg CodigoAfip, CodigoRespuestaEnvioSAP CodigoSAP,EstadoEnAFIP,EstadoEnSAP 
	from 
		solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and ctg <> '' 
		and ctg is not null 
		and EstadoEnAFIP in (1,4,5,6,8,9) 
		and numerocartadeporte not in 
			(select numerocartadeporte from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnSAP in (2)) 
		and FechaDeEmision between @FechaDesde and @FechaHasta
	
	UNION ALL
	
	select Convert(varchar,FechaDeEmision,111) Fecha, numerocartadeporte,Ctg CodigoAfip, CodigoRespuestaEnvioSAP CodigoSAP,EstadoEnAFIP,EstadoEnSAP 
	from 
		solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and ctg <> '' 
		and ctg is not null 
		and EstadoEnSAP in (2) 
		and numerocartadeporte not in 
			(select numerocartadeporte from solicitudes where IdEmpresa = @IdEmpresa and ctg <> '' and ctg is not null and EstadoEnAFIP in (1,4,5,6,8,9)) 
		and FechaDeEmision between @FechaDesde and @FechaHasta
	
	
	order by 
		fecha,numerocartadeporte

END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudTop')
	DROP PROCEDURE [GetSolicitudTop]
GO
CREATE PROCEDURE [dbo].[GetSolicitudTop] 
(
	@Top int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(75)

	SET @columnList = '*'
	SET @sqlCommand = 'SELECT top ' + CONVERT(varchar,@Top) + ' ' + @columnList + ' from	Solicitudes where s.IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' order by 1 desc'

	EXEC (@sqlCommand)	


END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudTopConfirmacion')
	DROP PROCEDURE [GetSolicitudTopConfirmacion]
GO
CREATE PROCEDURE [dbo].[GetSolicitudTopConfirmacion] 
(
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
		
	select TOP 8 * 
	from 
		solicitudes 
	where 
		IdEmpresa = @IdEmpresa
		and EstadoEnAFIP = 9		
	
	UNION ALL

	select	TOP 8 s.*
	from	
		solicitudes s, Empresa e, cliente c,establecimiento es
	where	
			s.IdEmpresa = @IdEmpresa
	and		s.EstadoEnAFIP = 1
	and		s.IdEstablecimientoDestino = es.IdEstablecimiento
	and		c.IdCliente = e.IdCliente
	and		es.IdInterlocutorDestinatario = c.IdCliente
	and		s.ObservacionAfip <> 'CTG Otorgado Carga Masiva'


END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarSolicitud')
	DROP PROCEDURE [GuardarSolicitud]
GO
CREATE PROCEDURE [dbo].[GuardarSolicitud]
(
	@IdSolicitud int,
	@IdTipoDeCarta int,
	@ObservacionAfip nvarchar(4000),
	@NumeroCartaDePorte nvarchar(40),
	@Cee nvarchar(40),
	@Ctg nvarchar(40),
	@FechaDeEmision datetime,
	@FechaDeCarga datetime,
	@FechaDeVencimiento datetime,
	@IdProveedorTitularCartaDePorte int,
	@IdClienteIntermediario int,
	@IdClienteRemitenteComercial int,
	@RemitenteComercialComoCanjeador bit,
	@IdClienteCorredor int,
	@IdClienteEntregador int,
	@IdClienteDestinatario int,
	@IdClienteDestino int,
	@IdProveedorTransportista int,
	@IdChoferTransportista int,
	@IdChofer int,
	@IdCosecha int,
	@IdEspecie int,
	@NumeroContrato int,
	@SapContrato int,
	@SinContrato bit,
	@CargaPesadaDestino bit,
	@KilogramosEstimados decimal(18,2),
	@DeclaracionDeCalidad nvarchar(100),
	@IdConformeCondicional int,
	@PesoBruto decimal,
	@PesoTara decimal,
	@Observaciones nvarchar(4000),
	@LoteDeMaterial nvarchar(100),
	@IdEstablecimientoProcedencia int,
	@IdEstablecimientoDestino int,
	@PatenteCamion nvarchar(30),
	@PatenteAcoplado nvarchar(30),
	@KmRecorridos decimal(18,2),
	@EstadoFlete int,
	@CantHoras decimal(18,2),
	@TarifaReferencia decimal(18,2),
	@TarifaReal decimal(18,2),
	@IdClientePagadorDelFlete int,
	@EstadoEnSAP int,
	@EstadoEnAFIP int,
	@IdGrano int,
	@CodigoAnulacionAfip decimal(18,0),
	@FechaAnulacionAfip datetime,
	@CodigoRespuestaEnvioSAP nvarchar(50),
    @CodigoRespuestaAnulacionSAP nvarchar(50),
	@MensajeRespuestaEnvioSAP nvarchar(500),
    @MensajeRespuestaAnulacionSAP nvarchar(500),
	@IdEstablecimientoDestinoCambio int,
	@IdClienteDestinatarioCambio int,
	@Usuario nvarchar(200),
	@IdEmpresa int

)

AS
BEGIN




	SET NOCOUNT ON;
	--declare @RemitenteComercialComoCanjeador bit
	--select @RemitenteComercialComoCanjeador = 0

	IF @IdSolicitud > 0
	BEGIN

		UPDATE	Solicitudes
		SET		IdTipoDeCarta	=	@IdTipoDeCarta,
				ObservacionAfip	=	@ObservacionAfip,
				NumeroCartaDePorte	=	@NumeroCartaDePorte,
				Cee	=	@Cee,
				Ctg	=	@Ctg,
				FechaDeEmision	=	@FechaDeEmision,
				FechaDeCarga	=	@FechaDeCarga,
				FechaDeVencimiento	=	@FechaDeVencimiento,
				IdProveedorTitularCartaDePorte	=	@IdProveedorTitularCartaDePorte,
				IdClienteIntermediario	=	@IdClienteIntermediario,
				IdClienteRemitenteComercial	=	@IdClienteRemitenteComercial,
				RemitenteComercialComoCanjeador = @RemitenteComercialComoCanjeador,
				IdClienteCorredor	=	@IdClienteCorredor,
				IdClienteEntregador	=	@IdClienteEntregador,
				IdClienteDestinatario	=	@IdClienteDestinatario,
				IdClienteDestino	=	@IdClienteDestino,
				IdProveedorTransportista	=	@IdProveedorTransportista,
				IdChoferTransportista = @IdChoferTransportista,
				IdChofer	=	@IdChofer,
				IdCosecha	=	@IdCosecha,
				IdEspecie	=	@IdEspecie,
				NumeroContrato	=	@NumeroContrato,
				SapContrato	=	@SapContrato,
				SinContrato	=	@SinContrato,
				CargaPesadaDestino	=	@CargaPesadaDestino,
				KilogramosEstimados	=	@KilogramosEstimados,
				DeclaracionDeCalidad	=	@DeclaracionDeCalidad,
				IdConformeCondicional	=	@IdConformeCondicional,
				PesoBruto	=	@PesoBruto,
				PesoTara	=	@PesoTara,
				Observaciones	=	@Observaciones,
				LoteDeMaterial	=	@LoteDeMaterial,
				IdEstablecimientoProcedencia	=	@IdEstablecimientoProcedencia,
				IdEstablecimientoDestino	=	@IdEstablecimientoDestino,
				PatenteCamion	=	@PatenteCamion,
				PatenteAcoplado	=	@PatenteAcoplado,
				KmRecorridos	=	@KmRecorridos,
				EstadoFlete	=	@EstadoFlete,
				CantHoras	=	@CantHoras,
				TarifaReferencia	=	@TarifaReferencia,
				TarifaReal	=	@TarifaReal,
				IdClientePagadorDelFlete	=	@IdClientePagadorDelFlete,
				EstadoEnSAP	=	@EstadoEnSAP,
				EstadoEnAFIP = @EstadoEnAFIP,
				IdGrano	=	@IdGrano,		
				CodigoAnulacionAfip = @CodigoAnulacionAfip,
				FechaAnulacionAfip = @FechaAnulacionAfip,
				CodigoRespuestaEnvioSAP = @CodigoRespuestaEnvioSAP,
				CodigoRespuestaAnulacionSAP = @CodigoRespuestaAnulacionSAP,
				MensajeRespuestaEnvioSAP = @MensajeRespuestaEnvioSAP,
				MensajeRespuestaAnulacionSAP = @MensajeRespuestaAnulacionSAP,
				FechaModificacion = getDate(),
				IdEstablecimientoDestinoCambio = @IdEstablecimientoDestinoCambio,
				IdClienteDestinatarioCambio = @IdClienteDestinatarioCambio,
				UsuarioModificacion = @Usuario
		WHERE	IdSolicitud	=	@IdSolicitud


		insert into logsolicitudes SELECT *,'UPDATE' as MOTIVO FROM Solicitudes where IdSolicitud	=	@IdSolicitud

	END
	ELSE
	BEGIN

		INSERT INTO Solicitudes (IdTipoDeCarta,	ObservacionAfip,NumeroCartaDePorte,	Cee,
		Ctg,FechaDeEmision,	FechaDeCarga,	FechaDeVencimiento,		IdProveedorTitularCartaDePorte,
		IdClienteIntermediario,	IdClienteRemitenteComercial,	RemitenteComercialComoCanjeador, IdClienteCorredor,	IdClienteEntregador,		IdClienteDestinatario,		IdClienteDestino,
		IdProveedorTransportista,IdChoferTransportista,	IdChofer,	IdCosecha,	IdEspecie,	NumeroContrato,
		SapContrato,	SinContrato,	CargaPesadaDestino,	KilogramosEstimados,	DeclaracionDeCalidad,
		IdConformeCondicional,	PesoBruto,	PesoTara,	Observaciones,	LoteDeMaterial,
		IdEstablecimientoProcedencia,	IdEstablecimientoDestino,	PatenteCamion,	PatenteAcoplado,
		KmRecorridos,	EstadoFlete,	CantHoras,	TarifaReferencia,	TarifaReal,
		IdClientePagadorDelFlete,	EstadoEnSAP, EstadoEnAFIP, IdGrano, CodigoAnulacionAfip, FechaAnulacionAfip, CodigoRespuestaEnvioSAP, CodigoRespuestaAnulacionSAP, MensajeRespuestaEnvioSAP, MensajeRespuestaAnulacionSAP, IdEstablecimientoDestinoCambio, IdClienteDestinatarioCambio, FechaCreacion,	UsuarioCreacion, IdEmpresa) 
		VALUES 
		(@IdTipoDeCarta,	@ObservacionAfip,@NumeroCartaDePorte,	@Cee,
		@Ctg,@FechaDeEmision,	@FechaDeCarga,	@FechaDeVencimiento,		@IdProveedorTitularCartaDePorte,
		@IdClienteIntermediario,	@IdClienteRemitenteComercial,	@RemitenteComercialComoCanjeador, @IdClienteCorredor,	@IdClienteEntregador,		@IdClienteDestinatario,		@IdClienteDestino,
		@IdProveedorTransportista, @IdChoferTransportista,	@IdChofer,	@IdCosecha,	@IdEspecie,	@NumeroContrato,
		@SapContrato,	@SinContrato,	@CargaPesadaDestino,	@KilogramosEstimados,	@DeclaracionDeCalidad,
		@IdConformeCondicional,	@PesoBruto,	@PesoTara,	@Observaciones,	@LoteDeMaterial,
		@IdEstablecimientoProcedencia,	@IdEstablecimientoDestino,	@PatenteCamion,	@PatenteAcoplado,
		@KmRecorridos,	@EstadoFlete,	@CantHoras,	@TarifaReferencia,	@TarifaReal,
		@IdClientePagadorDelFlete,	@EstadoEnSAP, @EstadoEnAFIP,@IdGrano, @CodigoAnulacionAfip, @FechaAnulacionAfip, @CodigoRespuestaEnvioSAP, @CodigoRespuestaAnulacionSAP, @MensajeRespuestaEnvioSAP, @MensajeRespuestaAnulacionSAP, @IdEstablecimientoDestinoCambio, @IdClienteDestinatarioCambio, getDate(),@Usuario, @IdEmpresa)
		
		DECLARE @scope int
		select @scope = SCOPE_IDENTITY()

		insert into logsolicitudes SELECT *,'INSERT' as MOTIVO FROM Solicitudes where IdSolicitud	= @scope

		SELECT @scope
	END


END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetCuitAutoComplete')
	DROP PROCEDURE [GetCuitAutoComplete]
GO
CREATE PROCEDURE [dbo].[GetCuitAutoComplete] 
(
	@campo nvarchar(100),
	@texto nvarchar(100),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = @campo + ' as cuit '
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT distinct top 10 ' + @columnList + 
					' from	SolicitudesRecibidas ' +
					' where IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and ' + @campo + ' like ' + @Valor 



	--PRINT @sqlCommand
	EXEC (@sqlCommand)

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudRecibida')
	DROP PROCEDURE [GetSolicitudRecibida]
GO
CREATE PROCEDURE [dbo].[GetSolicitudRecibida]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = 0
	BEGIN

		SELECT * 
		FROM 
			SolicitudesRecibidas 
		WHERE
			IdEmpresa = @IdEmpresa
		ORDER BY 1 desc

	END
	ELSE
	BEGIN

		SELECT * FROM SolicitudesRecibidas WHERE IdSolicitudRecibida = @Id

	END
	
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudRecibidaFiltro')
	DROP PROCEDURE [GetSolicitudRecibidaFiltro]
GO
CREATE PROCEDURE [dbo].[GetSolicitudRecibidaFiltro]
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = '* '
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT top 10000 ' + @columnList + 
					' from	SolicitudesRecibidas ' +
					' where IdEmpresa = ' + CAST(@IdEmpresa AS VARCHAR) + ' and (NumeroCartaDePorte like ' + @Valor + 
					' or	Ctg like ' + @Valor + 
					' or	UsuarioCreacion like ' + @Valor + ') order by 1 desc'

	

	--PRINT @sqlCommand
	EXEC (@sqlCommand)

END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudReporteRecibidas')
	DROP PROCEDURE [GetSolicitudReporteRecibidas]
GO
CREATE PROCEDURE [dbo].[GetSolicitudReporteRecibidas]
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT	* 
	FROM	SolicitudesRecibidas 
	where	
		IdEmpresa = @IdEmpresa
		and FechaDeEmision between @FechaDesde and @FechaHasta
	order by 1 desc
	
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarSolicitudRecibida')
	DROP PROCEDURE [GuardarSolicitudRecibida]
GO
CREATE PROCEDURE [dbo].[GuardarSolicitudRecibida]
(
	@IdSolicitudRecibida int,
	@IdTipoDeCarta int,
	@NumeroCartaDePorte nvarchar(40),
	@Cee nvarchar(40),
	@Ctg nvarchar(40),
	@FechaDeEmision datetime,
	@idProveedorTitularCartaDePorte nvarchar(40),
	@IdClienteIntermediario nvarchar(40),
	@IdClienteRemitenteComercial nvarchar(40),
	@IdClienteCorredor nvarchar(40),
	@IdClienteEntregador nvarchar(40),
	@IdClienteDestinatario nvarchar(40),
	@IdClienteDestino nvarchar(40),
	@IdProveedorTransportista nvarchar(40),
	@IdChofer nvarchar(40),
	@IdCosecha int,
	@IdEspecie int,
	@NumeroContrato int,
	@CargaPesadaDestino bit,
	@KilogramosEstimados decimal(18,2),
	@IdConformeCondicional int,
	@PesoBruto decimal,
	@PesoTara decimal,
	@Observaciones nvarchar(4000),
	@CodigoEstablecimientoProcedencia nvarchar(40),
	@IdLocalidadEstablecimientoProcedencia int,
	@IdEstablecimientoDestino nvarchar(40),
	@PatenteCamion nvarchar(30),
	@PatenteAcoplado nvarchar(30),
	@KmRecorridos decimal(18,2),
	@EstadoFlete int,
	@CantHoras decimal(18,2),
	@TarifaReferencia decimal(18,2),
	@TarifaReal decimal(18,2),
	@IdGrano int,
	@CodigoEstablecimientoDestinoCambio nvarchar(50),
	@IdLocalidadEstablecimientoDestinoCambio int,
	@CuitEstablecimientoDestinoCambio nvarchar(50),
	@FechaDeDescarga datetime,
	@FechaDeArribo datetime,
	@PesoNetoDescarga decimal,
	@Usuario nvarchar(200),
	@IdEmpresa int

)

AS
BEGIN




	SET NOCOUNT ON;

	IF @IdSolicitudRecibida > 0
	BEGIN

		UPDATE	SolicitudesRecibidas
		SET		IdTipoDeCarta = @IdTipoDeCarta,
				NumeroCartaDePorte = @NumeroCartaDePorte,
				Cee = @Cee,
				Ctg= @Ctg,
				FechaDeEmision = @FechaDeEmision,
				idProveedorTitularCartaDePorte = @idProveedorTitularCartaDePorte,
				IdClienteIntermediario = @IdClienteIntermediario,
				IdClienteRemitenteComercial = @IdClienteRemitenteComercial,
				IdClienteCorredor = @IdClienteCorredor,
				IdClienteEntregador = @IdClienteEntregador,
				IdClienteDestinatario = @IdClienteDestinatario,
				IdClienteDestino = @IdClienteDestino,
				IdProveedorTransportista = @IdProveedorTransportista,
				IdChofer = @IdChofer,
				IdCosecha = @IdCosecha,
				IdEspecie = @IdEspecie,
				NumeroContrato = @NumeroContrato,
				CargaPesadaDestino = @CargaPesadaDestino,
				KilogramosEstimados = @KilogramosEstimados,
				IdConformeCondicional = @IdConformeCondicional,
				PesoBruto = @PesoBruto,
				PesoTara = @PesoTara,
				Observaciones = @Observaciones,
				CodigoEstablecimientoProcedencia = @CodigoEstablecimientoProcedencia,
				IdLocalidadEstablecimientoProcedencia = @IdLocalidadEstablecimientoProcedencia,
				IdEstablecimientoDestino = @IdEstablecimientoDestino,
				PatenteCamion = @PatenteCamion,
				PatenteAcoplado = @PatenteAcoplado,
				KmRecorridos = @KmRecorridos,
				EstadoFlete = @EstadoFlete,
				CantHoras = @CantHoras,
				TarifaReferencia = @TarifaReferencia,
				TarifaReal = @TarifaReal,
				IdGrano = @IdGrano,
				CodigoEstablecimientoDestinoCambio = @CodigoEstablecimientoDestinoCambio,
				IdLocalidadEstablecimientoDestinoCambio = @IdLocalidadEstablecimientoDestinoCambio,
				CuitEstablecimientoDestinoCambio = @CuitEstablecimientoDestinoCambio,
				FechaDeDescarga = @FechaDeDescarga,
				FechaDeArribo = @FechaDeArribo,
				PesoNetoDescarga = @PesoNetoDescarga,
				FechaModificacion = getdate(),
				UsuarioModificacion = @Usuario
		WHERE	IdSolicitudRecibida	=	@IdSolicitudRecibida


	END
	ELSE
	BEGIN

		INSERT INTO SolicitudesRecibidas 
			(IdTipoDeCarta,NumeroCartaDePorte,Cee,Ctg,FechaDeEmision,idProveedorTitularCartaDePorte,IdClienteIntermediario,IdClienteRemitenteComercial,IdClienteCorredor,IdClienteEntregador,IdClienteDestinatario,IdClienteDestino,IdProveedorTransportista,IdChofer,IdCosecha,IdEspecie,NumeroContrato,CargaPesadaDestino,KilogramosEstimados,IdConformeCondicional,PesoBruto,PesoTara,Observaciones,CodigoEstablecimientoProcedencia,IdLocalidadEstablecimientoProcedencia,IdEstablecimientoDestino,PatenteCamion,PatenteAcoplado,KmRecorridos,EstadoFlete,CantHoras,TarifaReferencia,TarifaReal,IdGrano,CodigoEstablecimientoDestinoCambio,IdLocalidadEstablecimientoDestinoCambio,CuitEstablecimientoDestinoCambio,FechaDeDescarga,FechaDeArribo,PesoNetoDescarga,FechaCreacion,UsuarioCreacion,IdEmpresa) 
		VALUES
			(@IdTipoDeCarta,@NumeroCartaDePorte,@Cee,@Ctg,@FechaDeEmision,@idProveedorTitularCartaDePorte,@IdClienteIntermediario,@IdClienteRemitenteComercial,@IdClienteCorredor,@IdClienteEntregador,@IdClienteDestinatario,@IdClienteDestino,@IdProveedorTransportista,@IdChofer,@IdCosecha,@IdEspecie,@NumeroContrato,@CargaPesadaDestino,@KilogramosEstimados,@IdConformeCondicional,@PesoBruto,@PesoTara,@Observaciones,@CodigoEstablecimientoProcedencia,@IdLocalidadEstablecimientoProcedencia,@IdEstablecimientoDestino,@PatenteCamion,@PatenteAcoplado,@KmRecorridos,@EstadoFlete,@CantHoras,@TarifaReferencia,@TarifaReal,@IdGrano,@CodigoEstablecimientoDestinoCambio,@IdLocalidadEstablecimientoDestinoCambio,@CuitEstablecimientoDestinoCambio,@FechaDeDescarga,@FechaDeArribo,@PesoNetoDescarga, getDate(),@Usuario,@IdEmpresa)
		
		DECLARE @scope int
		select @scope = SCOPE_IDENTITY()


		SELECT @scope
	END


END
GO










IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'AnularReservaCartaDePorte')
	DROP PROCEDURE [AnularReservaCartaDePorte]
GO
CREATE PROCEDURE [dbo].[AnularReservaCartaDePorte] 
(
	@nroCartaDePorte varchar(50),
	@UsuarioReserva nvarchar(100),
	@IdGrupoEmpresa int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	UPDATE	CartasDePorte
	SET		FechaReserva = null,
			UsuarioReserva = null
	WHERE	IdGrupoEmpresa = @IdGrupoEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte

	UPDATE	Solicitudes 
	SET		EstadoEnSAP = 4,
			EstadoEnAFIP = 3,
			ObservacionAfip = 'Reserva de Carta de Porte ANULADA'
	WHERE	IdEmpresa = @IdEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte


	SELECT @nroCartaDePorte

				
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'CancelarReservaCartaDePorte')
	DROP PROCEDURE [CancelarReservaCartaDePorte]
GO
CREATE PROCEDURE [dbo].[CancelarReservaCartaDePorte] 
(
	@nroCartaDePorte varchar(50),
	@UsuarioReserva nvarchar(100),
	@IdGrupoEmpresa int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	UPDATE	CartasDePorte
	SET		Estado = 0,
			FechaReserva = null,
			UsuarioReserva = null
	WHERE	IdGrupoEmpresa = @IdGrupoEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte

	DELETE FROM Solicitudes 
	WHERE	IdEmpresa = @IdEmpresa
			and NumeroCartaDePorte = @nroCartaDePorte

	SELECT @nroCartaDePorte

				
END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'CantidadCartasDePorteDisponibles')
	DROP PROCEDURE [CantidadCartasDePorteDisponibles]
GO
CREATE PROCEDURE [dbo].[CantidadCartasDePorteDisponibles]
(
	@IdGrupoEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;
	select	count(*) cnt 
	from	CartasDePorte, LoteCartasDePorte
	where	
			CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
	and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
	and		CartasDePorte.Estado = 0 
	and		LoteCartasDePorte.FechaVencimiento >= getdate()
	and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetCartaDePorteDisponible')
	DROP PROCEDURE [GetCartaDePorteDisponible]
GO
CREATE PROCEDURE [dbo].[GetCartaDePorteDisponible]
(
	@idEstablecimiento int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @idEstablecimiento = 0
	BEGIN

		SELECT	top 1 CartasDePorte.*
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is null
		order by LoteCartasDePorte.FechaVencimiento,CartasDePorte.NumeroCartaDePorte

	END
	ELSE
	BEGIN

		SELECT	top 1 CartasDePorte.*
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is not null
		and		LoteCartasDePorte.EstablecimientoOrigen = @idEstablecimiento
		order by LoteCartasDePorte.FechaVencimiento,CartasDePorte.NumeroCartaDePorte


	END


END
GO




IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetCartasDePorte')
	DROP PROCEDURE [GetCartasDePorte]
GO
CREATE PROCEDURE [dbo].[GetCartasDePorte]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * 
		FROM 
			CartasDePorte
		WHERE
			IdGrupoEmpresa = @IdGrupoEmpresa
	END
	ELSE
	BEGIN
		SELECT * 
		FROM 
			CartasDePorte 
		WHERE 
			IdGrupoEmpresa = @IdGrupoEmpresa
			and IdCartaDePorte = @Id
	END


END
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetDisponiblePorLote')
	DROP PROCEDURE [GetDisponiblePorLote]
GO
CREATE PROCEDURE [dbo].[GetDisponiblePorLote]
(
	@idLote int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT count(*) FROM CartasDePorte WHERE IdGrupoEmpresa = @IdGrupoEmpresa and IdLoteLoteCartasDePorte = @idLote and Estado = 0

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetLoteCartasDePorte')
	DROP PROCEDURE [GetLoteCartasDePorte]
GO
CREATE PROCEDURE [dbo].[GetLoteCartasDePorte]
(
	@IdGrupoEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	select * from LoteCartasDePorte 
	where
		IdGrupoEmpresa = @IdGrupoEmpresa
	order by 
		FechaVencimiento asc

END
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetLoteCartasDePorteReporte')
	DROP PROCEDURE [GetLoteCartasDePorteReporte]
GO
CREATE PROCEDURE [dbo].[GetLoteCartasDePorteReporte] 
(
	@FechaDesde datetime,
	@FechaHasta datetime,
	@IdGrupoEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	select * from 
		LoteCartasDePorte 
	where 
		IdGrupoEmpresa = @IdGrupoEmpresa
		and fechacreacion between @FechaDesde and @FechaHasta 
	order by 
		FechaVencimiento asc

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetMisReservas')
	DROP PROCEDURE [GetMisReservas]
GO
CREATE PROCEDURE [dbo].[GetMisReservas] 
(
	@UsuarioReserva nvarchar(100),
	@IdGrupoEmpresa int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	IF @UsuarioReserva = ''
	BEGIN
		select	s.* 
		from	CartasDePorte c, solicitudes s	
		where	c.UsuarioReserva is not null
		and		s.ctg is null
		and		c.NumeroCartaDePorte = s.NumeroCartaDePorte
		and		s.IdEmpresa = @IdEmpresa
		and		c.IdGrupoEmpresa = @IdGrupoEmpresa
	END
	ELSE
	BEGIN
		select	s.* 
		from	CartasDePorte c, solicitudes s	
		where	c.UsuarioReserva = @UsuarioReserva 
		and		s.ctg is null
		and		c.NumeroCartaDePorte = s.NumeroCartaDePorte			
		and		s.IdEmpresa = @IdEmpresa
		and		c.IdGrupoEmpresa = @IdGrupoEmpresa
	END




END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarCartasDePorte')
	DROP PROCEDURE [GuardarCartasDePorte]
GO
CREATE PROCEDURE [dbo].[GuardarCartasDePorte]
(
      @Desde varchar(50),
      @Hasta varchar(50),
      @NroCEE varchar(50),
      @FechaVencimiento datetime,
      @Usuario nvarchar(50),
      @EstablecimientoOrigen varchar(50) = null,
	  @IdGrupoEmpresa int
)

AS
BEGIN
      SET NOCOUNT ON;

      DECLARE @loteID int
      INSERT INTO LoteCartasDePorte 
		(Desde, Hasta, Cee, FechaVencimiento,EstablecimientoOrigen,UsuarioCreacion, IdGrupoEmpresa)  
	  VALUES
		(@Desde, @Hasta, @NroCEE, @FechaVencimiento,@EstablecimientoOrigen,@Usuario, @IdGrupoEmpresa)

      SELECT @loteID = SCOPE_IDENTITY()

      
      DECLARE @desdeINT int
      DECLARE @hastaINT int
      DECLARE @cnt int

      SET @desdeINT = CONVERT(int,@Desde)
      SET @hastaINT = CONVERT(int,@Hasta)
      SET @cnt = 0

      WHILE @desdeINT <= @hastaINT
      BEGIN
        IF NOT EXISTS
        (
                  SELECT NumeroCartaDePorte
                  FROM CartasDePorte
                  WHERE NumeroCartaDePorte = CONVERT(VARCHAR,@desdeINT)
				  and IdGrupoEmpresa = @IdGrupoEmpresa
        )
        BEGIN
                  INSERT INTO CartasDePorte 
					(NumeroCartaDePorte,NumeroCee,Estado,IdLoteLoteCartasDePorte, IdGrupoEmpresa) 
				  VALUES
					(CONVERT(VARCHAR,@desdeINT),@NroCEE,0,@loteID, @IdGrupoEmpresa)

                  SET @cnt = @cnt + 1
        END

            SET @desdeINT = @desdeINT + 1  

      END

      select @cnt

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarEstadoCartaDePorte')
	DROP PROCEDURE [GuardarEstadoCartaDePorte]
GO
CREATE PROCEDURE [dbo].[GuardarEstadoCartaDePorte]
(
	@Id int,
	@estado int,
	@IdGrupoEmpresa int
)

AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	cartasdeporte
	SET		Estado = @estado
	WHERE	IdCartaDePorte = @Id 
			and IdGrupoEmpresa = @IdGrupoEmpresa

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ReservaCartaDePorte')
	DROP PROCEDURE [ReservaCartaDePorte]
GO
CREATE PROCEDURE [dbo].[ReservaCartaDePorte]
(
	@UsuarioReserva nvarchar(100),
	@IdEstablecimientoOrigen int,
	@IdTipoCartaDePorte int,
	@IdGrupoEmpresa int,
	@IdEmpresa int
)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @cartaID int
	DECLARE @cartaDePorte varchar(50)
	DECLARE @cee varchar(50)
	DECLARE @asocia int

	-- Chequeo si el establecimiento debe tener o no cartas de porte asignadas
	SELECT @asocia = AsociaCartaDePorte from establecimiento where idestablecimiento = @IdEstablecimientoOrigen

	IF @asocia = 0
	BEGIN

		SELECT	top 1 @cartaID = CartasDePorte.IdCartaDePorte , @cartaDePorte = CartasDePorte.NumeroCartaDePorte, @cee = CartasDePorte.NumeroCee
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is null
		order by LoteCartasDePorte.FechaVencimiento,CartasDePorte.NumeroCartaDePorte

	END
	ELSE
	BEGIN

		SELECT	top 1 @cartaID = CartasDePorte.IdCartaDePorte , @cartaDePorte = CartasDePorte.NumeroCartaDePorte, @cee = CartasDePorte.NumeroCee
		from	CartasDePorte ,LoteCartasDePorte
		where	
				CartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		LoteCartasDePorte.IdGrupoEmpresa = @IdGrupoEmpresa
		and		CartasDePorte.Estado = 0 
		and		LoteCartasDePorte.FechaVencimiento >= getdate()
		and		CartasDePorte.IdLoteLoteCartasDePorte = LoteCartasDePorte.IdLoteCartasDePorte
		and		LoteCartasDePorte.EstablecimientoOrigen is not null
		and		LoteCartasDePorte.EstablecimientoOrigen = @IdEstablecimientoOrigen
		order by LoteCartasDePorte.FechaVencimiento,CartasDePorte.NumeroCartaDePorte

	END


	IF @cartaDePorte is null
	BEGIN
		select 0
	END
	ELSE
	BEGIN

		UPDATE	CartasDePorte 
		SET		Estado = 1,
				FechaReserva = getdate(),
				UsuarioReserva = @UsuarioReserva
		WHERE	IdGrupoEmpresa = @IdGrupoEmpresa 
				and IdCartaDePorte = @cartaID


		INSERT INTO Solicitudes 
			(IdEstablecimientoProcedencia, ObservacionAfip,NumeroCartaDePorte,	Cee, FechaCreacion,	UsuarioCreacion,IdTipoDeCarta, IdEmpresa) 
		VALUES 
			(@IdEstablecimientoOrigen, 'Reserva de Carta de Porte', @cartaDePorte , @cee,getDate(),@UsuarioReserva,@IdTipoCartaDePorte, @IdEmpresa)

		select @cartaID

	END



END
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetEmpresaBySap_Id')
	DROP PROCEDURE [GetEmpresaBySap_Id]
GO
CREATE PROCEDURE [dbo].[GetEmpresaBySap_Id]
(
	@Id varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON;

	IF CONVERT(numeric(18), @Id) = 0
	BEGIN
		SELECT * FROM Empresa
	END
	ELSE
	BEGIN
		SELECT * FROM Empresa WHERE Sap_Id = @Id
	END


END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetEmpresaAdmin')
	DROP PROCEDURE [GetEmpresaAdmin]
GO
CREATE PROCEDURE [dbo].[GetEmpresaAdmin]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT 
			e.*, ge.IdApp, ge.IdPais
		FROM 
			Empresa e
			INNER JOIN GrupoEmpresa ge
				ON e.IdGrupoEmpresa = ge.IdGrupoEmpresa
	END
	ELSE
	BEGIN
		SELECT 
			e.*, ge.IdApp, ge.IdPais
		FROM 
			Empresa e
			INNER JOIN GrupoEmpresa ge
				ON e.IdGrupoEmpresa = ge.IdGrupoEmpresa
		WHERE 
			IdEmpresa = @Id
	END


END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudMeFiltro')
	DROP PROCEDURE [GetSolicitudMeFiltro]
GO
CREATE PROCEDURE [dbo].[GetSolicitudMeFiltro]
(
	@texto nvarchar(500),
	@estadoAFIP nvarchar(100),
	@estadoSAP nvarchar(100)
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand nvarchar(4000)
	DECLARE @columnList varchar(300)
	DECLARE @Valor varchar(500)

	SET @columnList = 's.*,e.Descripcion DescripcionEstablecimientoOrigen,tc.Descripcion as DescripcionTipoDeCarta,p2.Nombre as ProveedorNombreTitularCartaDePorte, s.IdEmpresa'
	SET @Valor = '''%' + @texto + '%'''

	SET @sqlCommand = ''
	SET @sqlCommand = @sqlCommand + 'SELECT top 100000 ' + @columnList + 
					' from	solicitudes s INNER JOIN establecimiento e ON s.IdEstablecimientoProcedencia = e.IdEstablecimiento	' + 
					' INNER JOIN TipoDeCarta tc ON tc.IdTipoDeCarta = s.IdTipoDeCarta ' +	
					' LEFT OUTER JOIN Proveedor p ON p.IdProveedor = s.IdEstablecimientoProcedencia ' +
					' LEFT OUTER JOIN Proveedor p2 ON p2.IdProveedor = s.idProveedorTitularCartaDePorte ' +
					' where (s.NumeroCartaDePorte like ' + @Valor + 
					' or		s.Ctg like ' + @Valor + 
					' or		s.ObservacionAfip like ' + @Valor + 
					' or		e.Descripcion like ' + @Valor + 
					' or		s.UsuarioCreacion like ' + @Valor + ') '

	IF @estadoAFIP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnAFIP in (' + @estadoAFIP + ')'
	END

	IF @estadoSAP <> '-1'
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND EstadoEnSAP in (' + @estadoSAP + ')'
	END

	SET @sqlCommand = @sqlCommand + ' order by 1 desc'

PRINT @sqlCommand

	EXEC (@sqlCommand)

END
GO
------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------









--------------------------------------------------------------------------------------------------------------------------
-- 2015-10-20
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









----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------

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
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetChoferUsadas')
	DROP PROCEDURE [GetChoferUsadas]
GO
CREATE PROCEDURE [dbo].[GetChoferUsadas]
(
	@IdGrupoEmpresa int
)
AS
BEGIN
	select	Chofer.* 
	from 	(select top 4 IdChofer,count(*) cnt from solicitudes group by IdChofer order by count(*) desc) tabla,
			Chofer
	where	tabla.IdChofer = Chofer.IdChofer
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
GO
----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------







----------------------------------------------------------------------------------------------------------------------------
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

	UPDATE	Chofer 
	SET		Activo = 0,
			FechaModificacion = getDate(),
			UsuarioModificacion = @Usuario
	WHERE	IdChofer = @IdChofer

END
GO

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------









------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSolicitudByCDP')
	DROP PROCEDURE [GetSolicitudByCDP]
GO

CREATE PROCEDURE [dbo].[GetSolicitudByCDP]
(
	@NumeroCartaDePorte nvarchar(40)
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Solicitudes 
	WHERE 
		NumeroCartaDePorte = @NumeroCartaDePorte
	
END
GO


------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------








--------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
if not exists (select * from sysobjects where name='LogOperaciones' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[LogOperaciones]
	(
		[IdLogOperaciones]	INT IDENTITY (1, 1) NOT NULL,
		[Tabla]				NVARCHAR (250) NULL,
		[Accion]		    NVARCHAR (250) NULL,
		[Id]                INT           NULL,
		[Fecha]				DATETIME      CONSTRAINT [DF_LogOperaciones_Fecha] DEFAULT (getdate()) NULL,
		[Usuario]			NVARCHAR (50) NULL,
		CONSTRAINT [PK_IdLogOperaciones] PRIMARY KEY CLUSTERED ([IdLogOperaciones] ASC)
	)
END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'EliminarCartasDePorte')
	DROP PROCEDURE [EliminarCartasDePorte]
GO
CREATE PROCEDURE [dbo].[EliminarCartasDePorte]
(
	@IdLoteCartasDePorte   INT,
	@Usuario varchar(150)
)

AS
BEGIN

	SET NOCOUNT ON;
    
	
	DELETE CartasDePorte 
	WHERE IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
    
	DELETE LoteCartasDePorte
	WHERE IdLoteCartasDePorte = @IdLoteCartasDePorte


	INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
	VALUES ('CartasDePorte', 'DELETE', @IdLoteCartasDePorte, GETDATE(), @Usuario)

	INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
	VALUES ('LoteCartasDePorte', 'DELETE', @IdLoteCartasDePorte, GETDATE(), @Usuario)

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'EliminarCartasDePorte')
	DROP PROCEDURE [EliminarCartasDePorteDisponibles]
GO
CREATE PROCEDURE [dbo].[EliminarCartasDePorteDisponibles]
(
	@IdLoteCartasDePorte   INT,
	@Usuario varchar(150)
)

AS
BEGIN

	SET NOCOUNT ON;
    

	DECLARE @UTILIZADO_HASTA INT;


	--busca ultimo utilizado
	SELECT 
		@UTILIZADO_HASTA = MAX(NumeroCartaDePorte) 
	FROM 
		CartasDePorte
	WHERE 
		IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
		AND Estado = 1;

	
	if (@UTILIZADO_HASTA > 0)
	BEGIN

		--elimina no utilizado
		DELETE CartasDePorte 
		WHERE 
			IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
			AND Estado = 0;


		--actualiza hasta del lote
		UPDATE LoteCartasDePorte
		SET Hasta = @UTILIZADO_HASTA
		WHERE IdLoteCartasDePorte = @IdLoteCartasDePorte


		INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
		VALUES ('CartasDePorte', 'DELETE DISP', @IdLoteCartasDePorte, GETDATE(), @Usuario)

		INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
		VALUES ('LoteCartasDePorte', 'DELETE DISP', @IdLoteCartasDePorte, GETDATE(), @Usuario)

	END	ELSE
	BEGIN

		--NO SE UTILIZO EL RANGO => ELIMINA EL LOTE COMPLETO		
		DELETE CartasDePorte 
		WHERE 
			IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
			AND Estado = 0;

		DELETE LoteCartasDePorte
		WHERE 
			IdLoteCartasDePorte = @IdLoteCartasDePorte

	END
END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetDisponiblePorLote')
	DROP PROCEDURE [GetDisponiblePorLote]
GO
CREATE PROCEDURE [dbo].[GetDisponiblePorLote]
(
	@idLote int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT count(*) FROM CartasDePorte WHERE IdLoteLoteCartasDePorte = @idLote and Estado = 0

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetLoteCartasDePorte')
	DROP PROCEDURE [GetLoteCartasDePorte]
GO
CREATE PROCEDURE [dbo].[GetLoteCartasDePorte]
AS
BEGIN
	SET NOCOUNT ON;
	
	select * from LoteCartasDePorte order by FechaVencimiento asc

END
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetLoteCartasDePorteFiltro')
	DROP PROCEDURE [GetLoteCartasDePorteFiltro]
GO
CREATE PROCEDURE [dbo].[GetLoteCartasDePorteFiltro] 
(
	@loteDesde int,
	@tieneDisponible int
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @sqlCommand varchar(1000)
	

	SET @sqlCommand = '';
	SET @sqlCommand = @sqlCommand + ' SELECT lcdp.*'
	SET @sqlCommand = @sqlCommand + ' FROM LoteCartasDePorte lcdp';
	SET @sqlCommand = @sqlCommand + ' WHERE 1=1 ';
	
	IF (@loteDesde > 0)
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND lcdp.Desde >= ' + CONVERT(varchar,@loteDesde) ;
	END

	IF (@tieneDisponible = 1)
	BEGIN
		SET @sqlCommand = @sqlCommand + ' AND lcdp.IdLoteCartasDePorte IN (SELECT TOP 1 cdp.IdLoteLoteCartasDePorte FROM CartasDePorte cdp WHERE cdp.Estado = 0 AND cdp.IdLoteLoteCartasDePorte = lcdp.IdLoteCartasDePorte)';
	END
	
	SET @sqlCommand = @sqlCommand + ' ORDER BY  lcdp.Desde';

	EXEC (@sqlCommand)

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetLoteCartasDePorteReporte')
	DROP PROCEDURE [GetLoteCartasDePorteReporte]
GO
CREATE PROCEDURE [dbo].[GetLoteCartasDePorteReporte] 
(
	@FechaDesde datetime,
	@FechaHasta datetime
)
AS
BEGIN
	SET NOCOUNT ON;
	
	select * from 
		LoteCartasDePorte 
	where 
		fechacreacion between @FechaDesde and @FechaHasta 
	order by 
		FechaVencimiento asc

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetUtilizadoPorLote')
	DROP PROCEDURE [GetUtilizadoPorLote]
GO
CREATE PROCEDURE [dbo].[GetUtilizadoPorLote]
(
	@idLote int
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT count(*) FROM CartasDePorte WHERE IdLoteLoteCartasDePorte = @idLote and Estado <> 0

END
GO


--------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------










----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetChoferUsadas')
	DROP PROCEDURE [GetChoferUsadas]
GO
CREATE PROCEDURE [dbo].[GetChoferUsadas]
(
	@IdGrupoEmpresa int
)
AS
BEGIN
	select	Chofer.* 
	from 	(select top 4 IdChofer,count(*) cnt 
				from solicitudes 
				where IdEmpresa in (select e.IdEmpresa from Empresa e where e.IdGrupoEmpresa = @IdGrupoEmpresa)
			 group by IdChofer order by count(*) desc) tabla,
			Chofer
	where	tabla.IdChofer = Chofer.IdChofer
			and Chofer.IdGrupoEmpresa = @IdGrupoEmpresa

--select * from solicitudes

END
GO










IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteCorredorUsadas')
	DROP PROCEDURE [GetClienteCorredorUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteCorredorUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteCorredor,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteCorredor order by count(*) desc) tabla,
			Cliente
	where	
			IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteCorredor = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

--select * from solicitudes

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteDestinatarioUsadas')
	DROP PROCEDURE [GetClienteDestinatarioUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteDestinatarioUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteDestinatario,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteDestinatario order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteDestinatario = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

--select * from solicitudes

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteDestinoUsadas')
	DROP PROCEDURE [GetClienteDestinoUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteDestinoUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteDestino,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteDestino order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteDestino = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)
--select * from solicitudes

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteEntregadorUsadas')
	DROP PROCEDURE [GetClienteEntregadorUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteEntregadorUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteEntregador,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteEntregador order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteEntregador = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

--select * from solicitudes

END
GO





IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteIntermediarioUsadas')
	DROP PROCEDURE [GetClienteIntermediarioUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteIntermediarioUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteIntermediario,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteIntermediario order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteIntermediario = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)
--select * from solicitudes

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClientePagadorDelFleteUsadas')
	DROP PROCEDURE [GetClientePagadorDelFleteUsadas]
GO
CREATE PROCEDURE [dbo].[GetClientePagadorDelFleteUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClientePagadorDelFlete,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClientePagadorDelFlete order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClientePagadorDelFlete = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetClienteRemitenteComercialUsadas')
	DROP PROCEDURE [GetClienteRemitenteComercialUsadas]
GO
CREATE PROCEDURE [dbo].[GetClienteRemitenteComercialUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteRemitenteComercial,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteRemitenteComercial order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteRemitenteComercial = Cliente.IdCliente
		and IdCliente not in (2000151,3000352)


--select * from solicitudes

END
GO






IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorTitularCartaDePorteUsadas')
	DROP PROCEDURE [GetProveedorTitularCartaDePorteUsadas]
GO
CREATE PROCEDURE [dbo].[GetProveedorTitularCartaDePorteUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Proveedor.* 
	from 	(select top 4 idProveedorTitularCartaDePorte,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by idProveedorTitularCartaDePorte order by count(*) desc) TitularCartaDePorte,
			Proveedor
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and TitularCartaDePorte.idProveedorTitularCartaDePorte = Proveedor.IdProveedor

END
GO







IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetProveedorTransportistaUsadas')
	DROP PROCEDURE [GetProveedorTransportistaUsadas]
GO
CREATE PROCEDURE [dbo].[GetProveedorTransportistaUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Proveedor.* 
	from 	(select top 4 IdProveedorTransportista,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdProveedorTransportista order by count(*) desc) tabla,
			Proveedor
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdProveedorTransportista = Proveedor.IdProveedor

--select * from solicitudes

END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GuardarSolicitud')
	DROP PROCEDURE [GuardarSolicitud]
GO
CREATE PROCEDURE [dbo].[GuardarSolicitud]
(
	@IdSolicitud int,
	@IdTipoDeCarta int,
	@ObservacionAfip nvarchar(4000),
	@NumeroCartaDePorte nvarchar(40),
	@Cee nvarchar(40),
	@Ctg nvarchar(40),
	@FechaDeEmision datetime,
	@FechaDeCarga datetime,
	@FechaDeVencimiento datetime,
	@IdProveedorTitularCartaDePorte int,
	@IdClienteIntermediario int,
	@IdClienteRemitenteComercial int,
	@RemitenteComercialComoCanjeador bit,
	@IdClienteCorredor int,
	@IdClienteEntregador int,
	@IdClienteDestinatario int,
	@IdClienteDestino int,
	@IdProveedorTransportista int,
	@IdChoferTransportista int,
	@IdChofer int,
	@IdCosecha int,
	@IdEspecie int,
	@NumeroContrato int,
	@SapContrato int,
	@SinContrato bit,
	@CargaPesadaDestino bit,
	@KilogramosEstimados decimal(18,2),
	@DeclaracionDeCalidad nvarchar(100),
	@IdConformeCondicional int,
	@PesoBruto decimal,
	@PesoTara decimal,
	@Observaciones nvarchar(4000),
	@LoteDeMaterial nvarchar(100),
	@IdEstablecimientoProcedencia int,
	@IdEstablecimientoDestino int,
	@PatenteCamion nvarchar(30),
	@PatenteAcoplado nvarchar(30),
	@KmRecorridos decimal(18,2),
	@EstadoFlete int,
	@CantHoras decimal(18,2),
	@TarifaReferencia decimal(18,2),
	@TarifaReal decimal(18,2),
	@IdClientePagadorDelFlete int,
	@EstadoEnSAP int,
	@EstadoEnAFIP int,
	@IdGrano int,
	@CodigoAnulacionAfip decimal(18,0),
	@FechaAnulacionAfip datetime,
	@CodigoRespuestaEnvioSAP nvarchar(50),
    @CodigoRespuestaAnulacionSAP nvarchar(50),
	@MensajeRespuestaEnvioSAP nvarchar(500),
    @MensajeRespuestaAnulacionSAP nvarchar(500),
	@IdEstablecimientoDestinoCambio int,
	@IdClienteDestinatarioCambio int,
	@Usuario nvarchar(200),
	@IdEmpresa int

)

AS
BEGIN




	SET NOCOUNT ON;
	--declare @RemitenteComercialComoCanjeador bit
	--select @RemitenteComercialComoCanjeador = 0

	IF @IdSolicitud > 0
	BEGIN

		UPDATE	Solicitudes
		SET		IdTipoDeCarta	=	@IdTipoDeCarta,
				ObservacionAfip	=	@ObservacionAfip,
				NumeroCartaDePorte	=	@NumeroCartaDePorte,
				Cee	=	@Cee,
				Ctg	=	@Ctg,
				FechaDeEmision	=	@FechaDeEmision,
				FechaDeCarga	=	@FechaDeCarga,
				FechaDeVencimiento	=	@FechaDeVencimiento,
				IdProveedorTitularCartaDePorte	=	@IdProveedorTitularCartaDePorte,
				IdClienteIntermediario	=	@IdClienteIntermediario,
				IdClienteRemitenteComercial	=	@IdClienteRemitenteComercial,
				RemitenteComercialComoCanjeador = @RemitenteComercialComoCanjeador,
				IdClienteCorredor	=	@IdClienteCorredor,
				IdClienteEntregador	=	@IdClienteEntregador,
				IdClienteDestinatario	=	@IdClienteDestinatario,
				IdClienteDestino	=	@IdClienteDestino,
				IdProveedorTransportista	=	@IdProveedorTransportista,
				IdChoferTransportista = @IdChoferTransportista,
				IdChofer	=	@IdChofer,
				IdCosecha	=	@IdCosecha,
				IdEspecie	=	@IdEspecie,
				NumeroContrato	=	@NumeroContrato,
				SapContrato	=	@SapContrato,
				SinContrato	=	@SinContrato,
				CargaPesadaDestino	=	@CargaPesadaDestino,
				KilogramosEstimados	=	@KilogramosEstimados,
				DeclaracionDeCalidad	=	@DeclaracionDeCalidad,
				IdConformeCondicional	=	@IdConformeCondicional,
				PesoBruto	=	@PesoBruto,
				PesoTara	=	@PesoTara,
				Observaciones	=	@Observaciones,
				LoteDeMaterial	=	@LoteDeMaterial,
				IdEstablecimientoProcedencia	=	@IdEstablecimientoProcedencia,
				IdEstablecimientoDestino	=	@IdEstablecimientoDestino,
				PatenteCamion	=	@PatenteCamion,
				PatenteAcoplado	=	@PatenteAcoplado,
				KmRecorridos	=	@KmRecorridos,
				EstadoFlete	=	@EstadoFlete,
				CantHoras	=	@CantHoras,
				TarifaReferencia	=	@TarifaReferencia,
				TarifaReal	=	@TarifaReal,
				IdClientePagadorDelFlete	=	@IdClientePagadorDelFlete,
				EstadoEnSAP	=	@EstadoEnSAP,
				EstadoEnAFIP = @EstadoEnAFIP,
				IdGrano	=	@IdGrano,		
				CodigoAnulacionAfip = @CodigoAnulacionAfip,
				FechaAnulacionAfip = @FechaAnulacionAfip,
				CodigoRespuestaEnvioSAP = @CodigoRespuestaEnvioSAP,
				CodigoRespuestaAnulacionSAP = @CodigoRespuestaAnulacionSAP,
				MensajeRespuestaEnvioSAP = @MensajeRespuestaEnvioSAP,
				MensajeRespuestaAnulacionSAP = @MensajeRespuestaAnulacionSAP,
				FechaModificacion = getDate(),
				IdEstablecimientoDestinoCambio = @IdEstablecimientoDestinoCambio,
				IdClienteDestinatarioCambio = @IdClienteDestinatarioCambio,
				UsuarioModificacion = @Usuario
		WHERE	IdSolicitud	=	@IdSolicitud


		insert into logsolicitudes 
		      ([IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa],[MOTIVO])
		SELECT [IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa]
				,'UPDATE' as MOTIVO 
		FROM Solicitudes 
		where IdSolicitud	= @IdSolicitud


	END
	ELSE
	BEGIN

		INSERT INTO Solicitudes (IdTipoDeCarta,	ObservacionAfip,NumeroCartaDePorte,	Cee,
		Ctg,FechaDeEmision,	FechaDeCarga,	FechaDeVencimiento,		IdProveedorTitularCartaDePorte,
		IdClienteIntermediario,	IdClienteRemitenteComercial,	RemitenteComercialComoCanjeador, IdClienteCorredor,	IdClienteEntregador,		IdClienteDestinatario,		IdClienteDestino,
		IdProveedorTransportista,IdChoferTransportista,	IdChofer,	IdCosecha,	IdEspecie,	NumeroContrato,
		SapContrato,	SinContrato,	CargaPesadaDestino,	KilogramosEstimados,	DeclaracionDeCalidad,
		IdConformeCondicional,	PesoBruto,	PesoTara,	Observaciones,	LoteDeMaterial,
		IdEstablecimientoProcedencia,	IdEstablecimientoDestino,	PatenteCamion,	PatenteAcoplado,
		KmRecorridos,	EstadoFlete,	CantHoras,	TarifaReferencia,	TarifaReal,
		IdClientePagadorDelFlete,	EstadoEnSAP, EstadoEnAFIP, IdGrano, CodigoAnulacionAfip, FechaAnulacionAfip, CodigoRespuestaEnvioSAP, CodigoRespuestaAnulacionSAP, MensajeRespuestaEnvioSAP, MensajeRespuestaAnulacionSAP, IdEstablecimientoDestinoCambio, IdClienteDestinatarioCambio, FechaCreacion,	UsuarioCreacion, IdEmpresa) 
		VALUES 
		(@IdTipoDeCarta,	@ObservacionAfip,@NumeroCartaDePorte,	@Cee,
		@Ctg,@FechaDeEmision,	@FechaDeCarga,	@FechaDeVencimiento,		@IdProveedorTitularCartaDePorte,
		@IdClienteIntermediario,	@IdClienteRemitenteComercial,	@RemitenteComercialComoCanjeador, @IdClienteCorredor,	@IdClienteEntregador,		@IdClienteDestinatario,		@IdClienteDestino,
		@IdProveedorTransportista, @IdChoferTransportista,	@IdChofer,	@IdCosecha,	@IdEspecie,	@NumeroContrato,
		@SapContrato,	@SinContrato,	@CargaPesadaDestino,	@KilogramosEstimados,	@DeclaracionDeCalidad,
		@IdConformeCondicional,	@PesoBruto,	@PesoTara,	@Observaciones,	@LoteDeMaterial,
		@IdEstablecimientoProcedencia,	@IdEstablecimientoDestino,	@PatenteCamion,	@PatenteAcoplado,
		@KmRecorridos,	@EstadoFlete,	@CantHoras,	@TarifaReferencia,	@TarifaReal,
		@IdClientePagadorDelFlete,	@EstadoEnSAP, @EstadoEnAFIP,@IdGrano, @CodigoAnulacionAfip, @FechaAnulacionAfip, @CodigoRespuestaEnvioSAP, @CodigoRespuestaAnulacionSAP, @MensajeRespuestaEnvioSAP, @MensajeRespuestaAnulacionSAP, @IdEstablecimientoDestinoCambio, @IdClienteDestinatarioCambio, getDate(),@Usuario, @IdEmpresa)
		
		DECLARE @scope int
		select @scope = SCOPE_IDENTITY()

		insert into logsolicitudes 
		      ([IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa],[MOTIVO])
		SELECT [IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa]
				,'INSERT' as MOTIVO 
		FROM Solicitudes 
		where IdSolicitud	= @scope

		SELECT @scope
	END


END
GO







----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
