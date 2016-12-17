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
