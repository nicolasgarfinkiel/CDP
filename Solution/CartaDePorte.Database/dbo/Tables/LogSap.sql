CREATE TABLE [dbo].[LogSap] (
    [IdLogSap]        INT           IDENTITY (1, 1) NOT NULL,
    [IDoc]            VARCHAR (50)  NULL,
    [Origen]          VARCHAR (50)  NULL,
    [NroDocumentoRE]  VARCHAR (50)  NULL,
    [NroDocumentoSap] VARCHAR (50)  NULL,
    [TipoMensaje]     VARCHAR (50)  NULL,
    [TextoMensaje]    VARCHAR (500) NULL,
    [NroEnvio]        INT           CONSTRAINT [DF_LogSap_NroEnvio] DEFAULT ((1)) NULL,
    [FechaCreacion]   DATETIME      CONSTRAINT [DF_LogSap_FechaCreacion] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_LogSap] PRIMARY KEY CLUSTERED ([IdLogSap] ASC)
);

