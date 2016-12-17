CREATE TABLE [dbo].[Empresa] (
    [IdEmpresa]                INT            IDENTITY (1, 1) NOT NULL,
    [IdCliente]                INT            NULL,
    [Descripcion]              NVARCHAR (200) NULL,
    [IdSapOrganizacionDeVenta] NVARCHAR (50)  NULL,
    [IdSapSector]              NVARCHAR (50)  NULL,
    [IdSapCanalLocal]          NVARCHAR (50)  NULL,
    [IdSapCanalExpor]          NVARCHAR (50)  NULL,
    [Sap_Id]                   NVARCHAR (50)  NULL,
	[IdGrupoEmpresa]		   INT NULL, 
	[IdSapMoneda]              NVARCHAR (5)  NULL,    CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC)
);

