CREATE TABLE [dbo].[Proveedor] (
    [IdProveedor]        INT            IDENTITY (1, 1) NOT NULL,
    [Sap_Id]             NVARCHAR (50)  NULL,
    [Nombre]             NVARCHAR (50)  NULL,
    [IdTipoDocumentoSAP] INT            NULL,
    [NumeroDocumento]    NVARCHAR (50)  NULL,
    [Calle]              NVARCHAR (200) NULL,
    [Piso]               NVARCHAR (50)  NULL,
    [Departamento]       NVARCHAR (50)  NULL,
    [Numero]             NVARCHAR (50)  NULL,
    [CP]                 NVARCHAR (50)  NULL,
    [Ciudad]             NVARCHAR (50)  NULL,
    [Pais]               NVARCHAR (50)  NULL,
    [Activo]             BIT            NULL,
    [Domicilio]          NVARCHAR (200) NULL,
    [FechaCreacion]      DATETIME       CONSTRAINT [DF_Proveedor_FechaCreacion] DEFAULT (getdate()) NULL,
    [EsProspecto]        BIT            CONSTRAINT [DF_Proveedor_EsProspecto] DEFAULT ((0)) NULL,
	[IdSapOrganizacionDeVenta] INT NOT NULL,
    CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED ([IdProveedor] ASC)
);

