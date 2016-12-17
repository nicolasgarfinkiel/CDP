CREATE TABLE [dbo].[TipoDocumentoSAP] (
    [IdTipoDocumentoSAP] INT          IDENTITY (1, 1) NOT NULL,
    [SAP_Id]             VARCHAR (2)  NULL,
    [Nombre]             VARCHAR (50) NULL,
    CONSTRAINT [PK_TipoDocumentoSAP] PRIMARY KEY CLUSTERED ([IdTipoDocumentoSAP] ASC)
);

