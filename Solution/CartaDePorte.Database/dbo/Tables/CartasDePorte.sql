CREATE TABLE [dbo].[CartasDePorte] (
    [IdCartaDePorte]          INT            IDENTITY (1, 1) NOT NULL,
    [NumeroCartaDePorte]      NVARCHAR (50)  NULL,
    [NumeroCee]               NVARCHAR (50)  NULL,
    [IdLoteLoteCartasDePorte] INT            NULL,
    [Estado]                  INT            NULL,
    [FechaReserva]            DATETIME       NULL,
    [UsuarioReserva]          NVARCHAR (100) NULL,
	[IdGrupoEmpresa] INT NOT NULL, 
    CONSTRAINT [PK_CartaDePorte] PRIMARY KEY CLUSTERED ([IdCartaDePorte] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_CartasDePorte_NumeroCartaDePorte]
    ON [dbo].[CartasDePorte]([NumeroCartaDePorte] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_CartasDePorte_IdLoteLoteCartasDePorte]
    ON [dbo].[CartasDePorte]([IdLoteLoteCartasDePorte] ASC);

