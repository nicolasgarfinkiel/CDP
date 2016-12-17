CREATE TABLE [dbo].[Establecimiento] (
    [IdEstablecimiento]          INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion]                NVARCHAR (250) NULL,
    [Direccion]                  NVARCHAR (250) NULL,
    [Localidad]                  INT            NULL,
    [Provincia]                  INT            NULL,
    [IDAlmacenSAP]               NVARCHAR (50)  NULL,
    [IDCentroSAP]                NVARCHAR (50)  NULL,
    [IdInterlocutorDestinatario] INT            NULL,
    [RecorridoEstablecimiento]   INT            NULL,
    [IDCEBE]                     NVARCHAR (50)  NULL,
    [IDExpedicion]               NVARCHAR (50)  NULL,
    [EstablecimientoAfip]        NVARCHAR (50)  NULL,
    [FechaCreacion]              DATETIME       NULL,
    [UsuarioCreacion]            NVARCHAR (150) NULL,
    [FechaModificacion]          DATETIME       NULL,
    [UsuarioModificacion]        NVARCHAR (150) NULL,
    [Activo]                     BIT            CONSTRAINT [DF_Establecimiento_Activo] DEFAULT ((1)) NULL,
    [AsociaCartaDePorte]         BIT            CONSTRAINT [DF_Establecimiento_AsociaCartaDePorte] DEFAULT ((0)) NOT NULL,
    [IdEmpresa] INT NOT NULL, 
    CONSTRAINT [PK_Establecimiento] PRIMARY KEY CLUSTERED ([IdEstablecimiento] ASC)
);

