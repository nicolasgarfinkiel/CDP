CREATE TABLE [dbo].[Chofer] (
    [IdChofer]              INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]                NVARCHAR (150) NULL,
    [Apellido]              NVARCHAR (150) NULL,
    [Cuit]                  NVARCHAR (50)  NULL,
    [Camion]                NVARCHAR (50)  NULL,
    [Acoplado]              NVARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME       NULL,
    [UsuarioCreacion]       NVARCHAR (100) NULL,
    [FechaModificacion]     DATETIME       NULL,
    [UsuarioModificacion]   NVARCHAR (100) NULL,
    [Activo]                BIT            CONSTRAINT [DF_Chofer_Activo] DEFAULT ((1)) NULL,
    [EsChoferTransportista] BIT            CONSTRAINT [DF_Chofer_EsChoferTransportista] DEFAULT ((0)) NULL,
    [IdGrupoEmpresa] INT NOT NULL, 
    CONSTRAINT [PK_Chofer] PRIMARY KEY CLUSTERED ([IdChofer] ASC)
);


GO
CREATE NONCLUSTERED INDEX [ix_Chofer_Cuit]
    ON [dbo].[Chofer]([Cuit] ASC);

