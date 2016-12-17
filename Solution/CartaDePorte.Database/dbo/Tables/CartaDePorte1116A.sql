CREATE TABLE [dbo].[CartaDePorte1116A](
	[IdCartaDePorte1116A] [int] NOT NULL,
	[IdSolicitud] [int] NOT NULL,
	[Numero1116A] [varchar](100) NOT NULL,
	[Fecha1116A] [datetime] NOT NULL,
	[UsuarioCreacion] [varchar](100) NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioModificacion] [varchar](100) NULL,
	[FechaModificacion] [datetime] NULL,
	[Activo] [int] NULL
) ON [PRIMARY]
