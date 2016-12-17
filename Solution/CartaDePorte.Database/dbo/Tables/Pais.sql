CREATE TABLE [dbo].[Pais]
(
	[IdPais]		 INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion]    NVARCHAR (250) NULL,
	CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED ([IdPais] ASC)
)
