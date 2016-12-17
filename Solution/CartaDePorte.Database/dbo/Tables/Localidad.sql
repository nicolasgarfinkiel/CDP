CREATE TABLE [dbo].[Localidad] (
    [IdProvincia] INT            NULL,
    [Codigo]      INT            NULL,
    [Descripcion] NVARCHAR (250) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Localidad_Provincia]
    ON [dbo].[Localidad]([IdProvincia] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Localidad_Codigo]
    ON [dbo].[Localidad]([Codigo] ASC);

