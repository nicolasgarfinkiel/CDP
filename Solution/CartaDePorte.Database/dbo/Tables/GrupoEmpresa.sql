CREATE TABLE [dbo].[GrupoEmpresa]
(
	[IdGrupoEmpresa]	 INT            IDENTITY (1, 1) NOT NULL,
	[Descripcion]    NVARCHAR (250) NULL,
	[Activo]         BIT            CONSTRAINT [DF_GrupoEmpresa_Activo] DEFAULT ((1)) NULL,
	[IdPais]         INT			NOT NULL, 
	[IdApp] INT NOT NULL, 
    CONSTRAINT [PK_GrupoEmpresa] PRIMARY KEY CLUSTERED ([IdGrupoEmpresa] ASC)
)
