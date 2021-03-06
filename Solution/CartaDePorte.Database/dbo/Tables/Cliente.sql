﻿CREATE TABLE [dbo].[Cliente] (
    [IdCliente]          NVARCHAR (20)  NOT NULL,
    [RazonSocial]        NVARCHAR (200) NULL,
    [NombreFantasia]     NVARCHAR (200) NULL,
    [Cuit]               NVARCHAR (20)  NULL,
    [IdTipoDocumentoSAP] INT            NULL,
    [IdClientePrincipal] INT            NULL,
    [Calle]              NVARCHAR (100) NULL,
    [Numero]             NVARCHAR (10)  NULL,
    [Dto]                NVARCHAR (10)  NULL,
    [Piso]               NVARCHAR (50)  NULL,
    [Cp]                 NVARCHAR (50)  NULL,
    [Poblacion]          NVARCHAR (50)  NULL,
    [Activo]             BIT            NULL,
    [GrupoComercial]     NVARCHAR (50)  NULL,
    [ClaveGrupo]         NVARCHAR (50)  NULL,
    [Tratamiento]        NVARCHAR (50)  NULL,
    [DescripcionGe]      NVARCHAR (50)  NULL,
    [FechaCreacion]      DATETIME       CONSTRAINT [DF_Cliente_FechaCreacion] DEFAULT (getdate()) NULL,
    [EsProspecto]        BIT            CONSTRAINT [DF_Cliente_EsProspecto] DEFAULT ((0)) NULL,
	[IdSapOrganizacionDeVenta] INT NOT NULL, 
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([IdCliente], [IdSapOrganizacionDeVenta]),
);

