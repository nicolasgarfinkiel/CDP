CREATE TABLE [dbo].[logInterfaz] (
    [Id]    INT             IDENTITY (1, 1) NOT NULL,
    [Texto] NVARCHAR (2000) NULL,
    [fecha] DATETIME        CONSTRAINT [DF_logInterfaz_fecha] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_logInterfaz] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [I_logInterfaz_fecha]
    ON [dbo].[logInterfaz]([fecha] ASC);

