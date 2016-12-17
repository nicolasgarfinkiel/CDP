CREATE TABLE [dbo].[Cosecha] (
    [IdCosecha]     INT            IDENTITY (1, 1) NOT NULL,
    [Codigo]        NVARCHAR (50)  NULL,
    [Descripcion]   NVARCHAR (150) NULL,
    [FechaCreacion] DATETIME       CONSTRAINT [DF_Cosecha_FechaCreacion] DEFAULT (getdate()) NULL,
	[IdGrupoEmpresa] INT NOT NULL, 
    CONSTRAINT [PK_Cosecha] PRIMARY KEY CLUSTERED ([IdCosecha] ASC)
);

