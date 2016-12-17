CREATE TABLE [dbo].[Partido] (
    [Codigo]      INT            NULL,
    [Descripcion] NVARCHAR (250) NULL,
	[IdProvincia] INT            NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Partido_Provincia]
    ON [dbo].[Partido]([IdProvincia] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Partido_Codigo]
    ON [dbo].[Partido]([Codigo] ASC);

