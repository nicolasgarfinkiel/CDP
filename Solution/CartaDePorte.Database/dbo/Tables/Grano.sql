CREATE TABLE [dbo].[Grano] (
    [IdGrano]             INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion]         NVARCHAR (250) NULL,
    [IdMaterialSap]       NVARCHAR (50)  NULL,
    [IdEspecieAfip]       INT            NULL,
    [IdCosechaAfip]       INT            NULL,
    [IdTipoGrano]         INT            NULL,
    [SujetoALote]         NVARCHAR (50)  NULL,
    [FechaCreacion]       DATETIME       NULL,
    [UsuarioCreacion]     NVARCHAR (150) NULL,
    [FechaModificacion]   DATETIME       NULL,
    [UsuarioModificacion] NVARCHAR (150) NULL,
    [Activo]              BIT            CONSTRAINT [DF_Grano_Activo] DEFAULT ((1)) NULL,
	[IdGrupoEmpresa] INT NOT NULL, 
    CONSTRAINT [PK_Grano] PRIMARY KEY CLUSTERED ([IdGrano] ASC)
);

