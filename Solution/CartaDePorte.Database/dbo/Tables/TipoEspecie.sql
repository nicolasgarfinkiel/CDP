CREATE TABLE [dbo].[TipoEspecie] (
    [IdTipoEspecie] INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion]   NVARCHAR (200) NULL,
    CONSTRAINT [PK_TipoEspecie] PRIMARY KEY CLUSTERED ([IdTipoEspecie] ASC)
);

