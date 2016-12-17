CREATE TABLE [dbo].[LoteCartasDePorte] (
    [IdLoteCartasDePorte]   INT           IDENTITY (1, 1) NOT NULL,
    [Desde]                 INT           NULL,
    [Hasta]                 INT           NULL,
    [Cee]                   NVARCHAR (50) NULL,
    [EstablecimientoOrigen] NVARCHAR (50) NULL,
    [FechaCreacion]         DATETIME      CONSTRAINT [DF_LoteCartasDePorte_FechaCreacion] DEFAULT (getdate()) NULL,
    [FechaVencimiento]      DATETIME      NULL,
    [UsuarioCreacion]       NVARCHAR (50) NULL,
	[IdGrupoEmpresa] INT NOT NULL, 
    CONSTRAINT [PK_LoteCartasDePorte] PRIMARY KEY CLUSTERED ([IdLoteCartasDePorte] ASC)
);

