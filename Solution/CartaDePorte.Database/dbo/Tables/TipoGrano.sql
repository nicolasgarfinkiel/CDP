CREATE TABLE [dbo].[TipoGrano] (
    [IdTipoGrano] INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion] NVARCHAR (150) NULL,
    CONSTRAINT [PK_TipoGrano] PRIMARY KEY CLUSTERED ([IdTipoGrano] ASC)
);

