CREATE TABLE [dbo].[Solicitudes] (
    [IdSolicitud]                     INT             IDENTITY (1, 1) NOT NULL,
    [IdTipoDeCarta]                   INT             NULL,
    [ObservacionAfip]                 NVARCHAR (4000) NULL,
    [NumeroCartaDePorte]              NVARCHAR (20)   NULL,
    [Cee]                             NVARCHAR (20)   NULL,
    [Ctg]                             NVARCHAR (20)   NULL,
    [FechaDeEmision]                  DATETIME        NULL,
    [FechaDeCarga]                    DATETIME        NULL,
    [FechaDeVencimiento]              DATETIME        NULL,
    [idProveedorTitularCartaDePorte]  INT             NULL,
    [IdClienteIntermediario]          INT             NULL,
    [IdClienteRemitenteComercial]     INT             NULL,
    [RemitenteComercialComoCanjeador] BIT             CONSTRAINT [DF_Solicitudes_RemitenteComercialComoCanjeador] DEFAULT ((0)) NULL,
    [IdClienteCorredor]               INT             NULL,
    [IdClienteEntregador]             INT             NULL,
    [IdClienteDestinatario]           INT             NULL,
    [IdClienteDestino]                INT             NULL,
    [IdProveedorTransportista]        INT             NULL,
    [IdChofer]                        INT             NULL,
    [IdCosecha]                       INT             NULL,
    [IdEspecie]                       INT             NULL,
    [NumeroContrato]                  INT             NULL,
    [SapContrato]                     INT             NULL,
    [SinContrato]                     BIT             NULL,
    [CargaPesadaDestino]              BIT             NULL,
    [KilogramosEstimados]             NUMERIC (18)    NULL,
    [DeclaracionDeCalidad]            NVARCHAR (50)   NULL,
    [IdConformeCondicional]           INT             NULL,
    [PesoBruto]                       NUMERIC (18)    NULL,
    [PesoTara]                        NUMERIC (18)    NULL,
    [PesoNeto]                        AS              ([PesoBruto]-[PesoTara]),
    [Observaciones]                   NVARCHAR (4000) NULL,
    [LoteDeMaterial]                  NVARCHAR (50)   NULL,
    [IdEstablecimientoProcedencia]    INT             NULL,
    [IdEstablecimientoDestino]        INT             NULL,
    [PatenteCamion]                   NVARCHAR (15)   NULL,
    [PatenteAcoplado]                 NVARCHAR (15)   NULL,
    [KmRecorridos]                    NUMERIC (18)    NULL,
    [EstadoFlete]                     INT             NULL,
    [CantHoras]                       NUMERIC (18)    NULL,
    [TarifaReferencia]                NUMERIC (18, 2) NULL,
    [TarifaReal]                      NUMERIC (18, 2) NULL,
    [IdClientePagadorDelFlete]        INT             NULL,
    [EstadoEnSAP]                     INT             CONSTRAINT [DF_Solicitudes_EstadoEnSAP] DEFAULT ((0)) NULL,
    [EstadoEnAFIP]                    INT             CONSTRAINT [DF_Solicitudes_EstadoEnAFIP] DEFAULT ((2)) NULL,
    [IdGrano]                         INT             NULL,
    [CodigoAnulacionAfip]             NUMERIC (18)    NULL,
    [FechaAnulacionAfip]              DATETIME        NULL,
    [CodigoRespuestaEnvioSAP]         NVARCHAR (50)   NULL,
    [MensajeRespuestaEnvioSAP]        NVARCHAR (500)  NULL,
    [CodigoRespuestaAnulacionSAP]     NVARCHAR (50)   NULL,
    [MensajeRespuestaAnulacionSAP]    NVARCHAR (500)  NULL,
    [IdEstablecimientoDestinoCambio]  INT             NULL,
    [IdClienteDestinatarioCambio]     INT             NULL,
    [FechaCreacion]                   DATETIME        NULL,
    [UsuarioCreacion]                 NVARCHAR (100)  NULL,
    [FechaModificacion]               DATETIME        NULL,
    [UsuarioModificacion]             NVARCHAR (100)  NULL,
    [IdChoferTransportista]           INT             NULL,
	[IdEmpresa] INT NOT NULL,
    CONSTRAINT [PK_Solicitudes] PRIMARY KEY CLUSTERED ([IdSolicitud] ASC),
    CONSTRAINT [FK_Solicitudes_TipoDeCarta] FOREIGN KEY ([IdTipoDeCarta]) REFERENCES [dbo].[TipoDeCarta] ([IdTipoDeCarta])
);


GO

CREATE TRIGGER trgUpdateSolicitudes ON solicitudes
FOR UPDATE
AS  
BEGIN

    --insert into logsolicitudes SELECT *,'TRIGGER' as MOTIVO FROM inserted
SELECT *,'TRIGGER' as MOTIVO FROM inserted
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Interno de la tabla autoenumerable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdSolicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Carta Enum Carta de Porte de venta prod Propia -Carta de Porte de venta prod de Terceros - Carta de Porte por movimiento de grano - Canje - CP de terceros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdTipoDeCarta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SOLICITUD DE EMISION DE CARTAS DE PORTE - Devuelve WS AFIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'ObservacionAfip';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nro Carta de porte - envia a SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'NumeroCartaDePorte';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se genera a partir del número de carta de porte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'Cee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Devuelve WS AFIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'Ctg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Por defecto Titular Carta de porte / permitir cliente. Si IdClientePagadorDelFlete es null entonces el pagador es La Empresa Titular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdClientePagadorDelFlete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Patente Camión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'PatenteCamion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Patente Acoplado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'PatenteAcoplado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enum Lista de valores posibles Flete Pagado - Flete a Pagar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'EstadoFlete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas para que salga el camión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'CantHoras';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa referncia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'TarifaReferencia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa real', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'TarifaReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enum Conforme = 1 Condicional = 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdConformeCondicional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'En Kgrs. Obligatorio con  "La carga será pesada en destino"  Destildado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'PesoBruto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso Tara en Kgrs Obligatorio - Camión mas acoplado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'PesoTara';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'([PesoBruto]-[PesoTara])', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'PesoNeto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se complerta si el grano es Aplicado en Lote - envia a SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'LoteDeMaterial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establecimiento Origen del Grano - ABM Establecimientos /Alta Manual - Equivalencia AFIP / SAP
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdEstablecimientoProcedencia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ABM Cosecha, tomando datos de AFIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdCosecha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tambien se pide TIPO... estaria como dato de la especie.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdEspecie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obligatorios con "Sin Contrato" Destildado - Enviar a SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'NumeroContrato';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CheckBox', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'SinContrato';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obligatorios con "La carga será pesada en destino" Tildado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'KilogramosEstimados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siempre va vacio. Quizas este campo se utilice en un futuro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'DeclaracionDeCalidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de Clientes de SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdClienteCorredor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de Clientes de SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdClienteEntregador';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de Clientes de SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdClienteDestinatario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de Clientes de SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdClienteDestino';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ABM Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdProveedorTransportista';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ABM Chofer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdChofer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fechaEmision - Devuelve WS AFIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'FechaDeEmision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Carga - Devuelve WS AFIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'FechaDeCarga';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vto - fechaVigenciaHasta - Devuelve WS AFIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'FechaDeVencimiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Titular Carta de Porte - Seleccionar de De Entidad Empresas. Lista de Empresas de SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'idProveedorTitularCartaDePorte';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de Clientes de SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdClienteIntermediario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de Empresas de SAP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Solicitudes', @level2type = N'COLUMN', @level2name = N'IdClienteRemitenteComercial';

