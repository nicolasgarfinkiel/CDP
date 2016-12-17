CREATE TABLE [dbo].[AfipAuth] (
    [IdAfipAuth]       INT             IDENTITY (1, 1) NOT NULL,
    [Token]            NVARCHAR (1000) NULL,
    [Sign]             NVARCHAR (1000) NULL,
    [CuitRepresentado] NVARCHAR (20)   NULL,
    [GenerationTime]   DATETIME        NULL,
    [ExpirationTime]   DATETIME        NULL,
    [Service]          NVARCHAR (50)   NULL,
    [UniqueID]         NVARCHAR (50)   NULL,
    CONSTRAINT [PK_AfipAuth] PRIMARY KEY CLUSTERED ([IdAfipAuth] ASC)
);

