CREATE TABLE [dbo].[Especie] (
    [IdEspecie]     INT            IDENTITY (1, 1) NOT NULL,
    [Codigo]        INT            NULL,
    [Descripcion]   NVARCHAR (150) NULL,
    [FechaCreacion] DATETIME       CONSTRAINT [DF_Especie_FechaCreacion] DEFAULT (getdate()) NULL,
	[IdGrupoEmpresa] INT NOT NULL,
    CONSTRAINT [PK_Especie] PRIMARY KEY CLUSTERED ([IdEspecie] ASC)
);

