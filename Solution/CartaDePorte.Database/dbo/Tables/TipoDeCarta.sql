CREATE TABLE [dbo].[TipoDeCarta] (
    [IdTipoDeCarta] INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion]   NVARCHAR (200) NULL,
    [Activo]        BIT            NULL,
    CONSTRAINT [PK_TipoDeCarta] PRIMARY KEY CLUSTERED ([IdTipoDeCarta] ASC)
);

