﻿CREATE TABLE [dbo].[sol2tmp] (
    [IdSolicitud]                    INT             IDENTITY (1, 1) NOT NULL,
    [IdTipoDeCarta]                  INT             NULL,
    [ObservacionAfip]                NVARCHAR (4000) NULL,
    [NumeroCartaDePorte]             NVARCHAR (20)   NULL,
    [Cee]                            NVARCHAR (20)   NULL,
    [Ctg]                            NVARCHAR (20)   NULL,
    [FechaDeEmision]                 DATETIME        NULL,
    [FechaDeCarga]                   DATETIME        NULL,
    [FechaDeVencimiento]             DATETIME        NULL,
    [idProveedorTitularCartaDePorte] INT             NULL,
    [IdClienteIntermediario]         INT             NULL,
    [IdEmpresaRemitenteComercial]    INT             NULL,
    [IdClienteCorredor]              INT             NULL,
    [IdClienteEntregador]            INT             NULL,
    [IdClienteDestinatario]          INT             NULL,
    [IdClienteDestino]               INT             NULL,
    [IdProveedorTransportista]       INT             NULL,
    [IdChofer]                       INT             NULL,
    [IdCosecha]                      INT             NULL,
    [IdEspecie]                      INT             NULL,
    [NumeroContrato]                 INT             NULL,
    [SapContrato]                    INT             NULL,
    [SinContrato]                    BIT             NULL,
    [CargaPesadaDestino]             BIT             NULL,
    [KilogramosEstimados]            NUMERIC (18)    NULL,
    [DeclaracionDeCalidad]           NVARCHAR (50)   NULL,
    [IdConformeCondicional]          INT             NULL,
    [PesoBruto]                      NUMERIC (18)    NULL,
    [PesoTara]                       NUMERIC (18)    NULL,
    [PesoNeto]                       NUMERIC (19)    NULL,
    [Observaciones]                  NVARCHAR (4000) NULL,
    [LoteDeMaterial]                 NVARCHAR (50)   NULL,
    [IdEstablecimientoProcedencia]   INT             NULL,
    [IdEstablecimientoDestino]       INT             NULL,
    [PatenteCamion]                  NVARCHAR (15)   NULL,
    [PatenteAcoplado]                NVARCHAR (15)   NULL,
    [KmRecorridos]                   NUMERIC (18)    NULL,
    [EstadoFlete]                    INT             NULL,
    [CantHoras]                      NUMERIC (18)    NULL,
    [TarifaReferencia]               NUMERIC (18, 2) NULL,
    [TarifaReal]                     NUMERIC (18, 2) NULL,
    [IdClientePagadorDelFlete]       INT             NULL,
    [EstadoEnSAP]                    INT             NULL,
    [EstadoEnAFIP]                   INT             NULL,
    [IdGrano]                        INT             NULL,
    [CodigoAnulacionAfip]            NUMERIC (18)    NULL,
    [FechaAnulacionAfip]             DATETIME        NULL,
    [CodigoRespuestaEnvioSAP]        NVARCHAR (50)   NULL,
    [MensajeRespuestaEnvioSAP]       NVARCHAR (500)  NULL,
    [CodigoRespuestaAnulacionSAP]    NVARCHAR (50)   NULL,
    [MensajeRespuestaAnulacionSAP]   NVARCHAR (500)  NULL,
    [IdEstablecimientoDestinoCambio] INT             NULL,
    [IdClienteDestinatarioCambio]    INT             NULL,
    [FechaCreacion]                  DATETIME        NULL,
    [UsuarioCreacion]                NVARCHAR (100)  NULL,
    [FechaModificacion]              DATETIME        NULL,
    [UsuarioModificacion]            NVARCHAR (100)  NULL
);

