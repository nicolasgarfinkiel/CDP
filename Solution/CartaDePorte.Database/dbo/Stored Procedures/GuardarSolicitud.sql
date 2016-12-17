CREATE PROCEDURE [dbo].[GuardarSolicitud]
(
	@IdSolicitud int,
	@IdTipoDeCarta int,
	@ObservacionAfip nvarchar(4000),
	@NumeroCartaDePorte nvarchar(40),
	@Cee nvarchar(40),
	@Ctg nvarchar(40),
	@FechaDeEmision datetime,
	@FechaDeCarga datetime,
	@FechaDeVencimiento datetime,
	@IdProveedorTitularCartaDePorte int,
	@IdClienteIntermediario int,
	@IdClienteRemitenteComercial int,
	@RemitenteComercialComoCanjeador bit,
	@IdClienteCorredor int,
	@IdClienteEntregador int,
	@IdClienteDestinatario int,
	@IdClienteDestino int,
	@IdProveedorTransportista int,
	@IdChoferTransportista int,
	@IdChofer int,
	@IdCosecha int,
	@IdEspecie int,
	@NumeroContrato int,
	@SapContrato int,
	@SinContrato bit,
	@CargaPesadaDestino bit,
	@KilogramosEstimados decimal(18,2),
	@DeclaracionDeCalidad nvarchar(100),
	@IdConformeCondicional int,
	@PesoBruto decimal,
	@PesoTara decimal,
	@Observaciones nvarchar(4000),
	@LoteDeMaterial nvarchar(100),
	@IdEstablecimientoProcedencia int,
	@IdEstablecimientoDestino int,
	@PatenteCamion nvarchar(30),
	@PatenteAcoplado nvarchar(30),
	@KmRecorridos decimal(18,2),
	@EstadoFlete int,
	@CantHoras decimal(18,2),
	@TarifaReferencia decimal(18,2),
	@TarifaReal decimal(18,2),
	@IdClientePagadorDelFlete int,
	@EstadoEnSAP int,
	@EstadoEnAFIP int,
	@IdGrano int,
	@CodigoAnulacionAfip decimal(18,0),
	@FechaAnulacionAfip datetime,
	@CodigoRespuestaEnvioSAP nvarchar(50),
    @CodigoRespuestaAnulacionSAP nvarchar(50),
	@MensajeRespuestaEnvioSAP nvarchar(500),
    @MensajeRespuestaAnulacionSAP nvarchar(500),
	@IdEstablecimientoDestinoCambio int,
	@IdClienteDestinatarioCambio int,
	@Usuario nvarchar(200),
	@IdEmpresa int

)

AS
BEGIN




	SET NOCOUNT ON;
	--declare @RemitenteComercialComoCanjeador bit
	--select @RemitenteComercialComoCanjeador = 0

	IF @IdSolicitud > 0
	BEGIN

		UPDATE	Solicitudes
		SET		IdTipoDeCarta	=	@IdTipoDeCarta,
				ObservacionAfip	=	@ObservacionAfip,
				NumeroCartaDePorte	=	@NumeroCartaDePorte,
				Cee	=	@Cee,
				Ctg	=	@Ctg,
				FechaDeEmision	=	@FechaDeEmision,
				FechaDeCarga	=	@FechaDeCarga,
				FechaDeVencimiento	=	@FechaDeVencimiento,
				IdProveedorTitularCartaDePorte	=	@IdProveedorTitularCartaDePorte,
				IdClienteIntermediario	=	@IdClienteIntermediario,
				IdClienteRemitenteComercial	=	@IdClienteRemitenteComercial,
				RemitenteComercialComoCanjeador = @RemitenteComercialComoCanjeador,
				IdClienteCorredor	=	@IdClienteCorredor,
				IdClienteEntregador	=	@IdClienteEntregador,
				IdClienteDestinatario	=	@IdClienteDestinatario,
				IdClienteDestino	=	@IdClienteDestino,
				IdProveedorTransportista	=	@IdProveedorTransportista,
				IdChoferTransportista = @IdChoferTransportista,
				IdChofer	=	@IdChofer,
				IdCosecha	=	@IdCosecha,
				IdEspecie	=	@IdEspecie,
				NumeroContrato	=	@NumeroContrato,
				SapContrato	=	@SapContrato,
				SinContrato	=	@SinContrato,
				CargaPesadaDestino	=	@CargaPesadaDestino,
				KilogramosEstimados	=	@KilogramosEstimados,
				DeclaracionDeCalidad	=	@DeclaracionDeCalidad,
				IdConformeCondicional	=	@IdConformeCondicional,
				PesoBruto	=	@PesoBruto,
				PesoTara	=	@PesoTara,
				Observaciones	=	@Observaciones,
				LoteDeMaterial	=	@LoteDeMaterial,
				IdEstablecimientoProcedencia	=	@IdEstablecimientoProcedencia,
				IdEstablecimientoDestino	=	@IdEstablecimientoDestino,
				PatenteCamion	=	@PatenteCamion,
				PatenteAcoplado	=	@PatenteAcoplado,
				KmRecorridos	=	@KmRecorridos,
				EstadoFlete	=	@EstadoFlete,
				CantHoras	=	@CantHoras,
				TarifaReferencia	=	@TarifaReferencia,
				TarifaReal	=	@TarifaReal,
				IdClientePagadorDelFlete	=	@IdClientePagadorDelFlete,
				EstadoEnSAP	=	@EstadoEnSAP,
				EstadoEnAFIP = @EstadoEnAFIP,
				IdGrano	=	@IdGrano,		
				CodigoAnulacionAfip = @CodigoAnulacionAfip,
				FechaAnulacionAfip = @FechaAnulacionAfip,
				CodigoRespuestaEnvioSAP = @CodigoRespuestaEnvioSAP,
				CodigoRespuestaAnulacionSAP = @CodigoRespuestaAnulacionSAP,
				MensajeRespuestaEnvioSAP = @MensajeRespuestaEnvioSAP,
				MensajeRespuestaAnulacionSAP = @MensajeRespuestaAnulacionSAP,
				FechaModificacion = getDate(),
				IdEstablecimientoDestinoCambio = @IdEstablecimientoDestinoCambio,
				IdClienteDestinatarioCambio = @IdClienteDestinatarioCambio,
				UsuarioModificacion = @Usuario
		WHERE	IdSolicitud	=	@IdSolicitud


		insert into logsolicitudes 
		      ([IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa],[MOTIVO])
		SELECT [IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa]
				,'UPDATE' as MOTIVO 
		FROM Solicitudes 
		where IdSolicitud	= @IdSolicitud


	END
	ELSE
	BEGIN

		INSERT INTO Solicitudes (IdTipoDeCarta,	ObservacionAfip,NumeroCartaDePorte,	Cee,
		Ctg,FechaDeEmision,	FechaDeCarga,	FechaDeVencimiento,		IdProveedorTitularCartaDePorte,
		IdClienteIntermediario,	IdClienteRemitenteComercial,	RemitenteComercialComoCanjeador, IdClienteCorredor,	IdClienteEntregador,		IdClienteDestinatario,		IdClienteDestino,
		IdProveedorTransportista,IdChoferTransportista,	IdChofer,	IdCosecha,	IdEspecie,	NumeroContrato,
		SapContrato,	SinContrato,	CargaPesadaDestino,	KilogramosEstimados,	DeclaracionDeCalidad,
		IdConformeCondicional,	PesoBruto,	PesoTara,	Observaciones,	LoteDeMaterial,
		IdEstablecimientoProcedencia,	IdEstablecimientoDestino,	PatenteCamion,	PatenteAcoplado,
		KmRecorridos,	EstadoFlete,	CantHoras,	TarifaReferencia,	TarifaReal,
		IdClientePagadorDelFlete,	EstadoEnSAP, EstadoEnAFIP, IdGrano, CodigoAnulacionAfip, FechaAnulacionAfip, CodigoRespuestaEnvioSAP, CodigoRespuestaAnulacionSAP, MensajeRespuestaEnvioSAP, MensajeRespuestaAnulacionSAP, IdEstablecimientoDestinoCambio, IdClienteDestinatarioCambio, FechaCreacion,	UsuarioCreacion, IdEmpresa) 
		VALUES 
		(@IdTipoDeCarta,	@ObservacionAfip,@NumeroCartaDePorte,	@Cee,
		@Ctg,@FechaDeEmision,	@FechaDeCarga,	@FechaDeVencimiento,		@IdProveedorTitularCartaDePorte,
		@IdClienteIntermediario,	@IdClienteRemitenteComercial,	@RemitenteComercialComoCanjeador, @IdClienteCorredor,	@IdClienteEntregador,		@IdClienteDestinatario,		@IdClienteDestino,
		@IdProveedorTransportista, @IdChoferTransportista,	@IdChofer,	@IdCosecha,	@IdEspecie,	@NumeroContrato,
		@SapContrato,	@SinContrato,	@CargaPesadaDestino,	@KilogramosEstimados,	@DeclaracionDeCalidad,
		@IdConformeCondicional,	@PesoBruto,	@PesoTara,	@Observaciones,	@LoteDeMaterial,
		@IdEstablecimientoProcedencia,	@IdEstablecimientoDestino,	@PatenteCamion,	@PatenteAcoplado,
		@KmRecorridos,	@EstadoFlete,	@CantHoras,	@TarifaReferencia,	@TarifaReal,
		@IdClientePagadorDelFlete,	@EstadoEnSAP, @EstadoEnAFIP,@IdGrano, @CodigoAnulacionAfip, @FechaAnulacionAfip, @CodigoRespuestaEnvioSAP, @CodigoRespuestaAnulacionSAP, @MensajeRespuestaEnvioSAP, @MensajeRespuestaAnulacionSAP, @IdEstablecimientoDestinoCambio, @IdClienteDestinatarioCambio, getDate(),@Usuario, @IdEmpresa)
		
		DECLARE @scope int
		select @scope = SCOPE_IDENTITY()

		insert into logsolicitudes 
		      ([IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa],[MOTIVO])
		SELECT [IdSolicitud],[IdTipoDeCarta],[ObservacionAfip],[NumeroCartaDePorte],[Cee],[Ctg],[FechaDeEmision],[FechaDeCarga],[FechaDeVencimiento],[idProveedorTitularCartaDePorte],[IdClienteIntermediario],[IdClienteRemitenteComercial],[RemitenteComercialComoCanjeador],[IdClienteCorredor],[IdClienteEntregador],[IdClienteDestinatario],[IdClienteDestino],[IdProveedorTransportista],[IdChofer],[IdCosecha],[IdEspecie],[NumeroContrato],[SapContrato],[SinContrato],[CargaPesadaDestino],[KilogramosEstimados],[DeclaracionDeCalidad],[IdConformeCondicional],[PesoBruto],[PesoTara],[PesoNeto],[Observaciones],[LoteDeMaterial],[IdEstablecimientoProcedencia],[IdEstablecimientoDestino],[PatenteCamion],[PatenteAcoplado],[KmRecorridos],[EstadoFlete],[CantHoras],[TarifaReferencia],[TarifaReal],[IdClientePagadorDelFlete],[EstadoEnSAP],[EstadoEnAFIP],[IdGrano],[CodigoAnulacionAfip],[FechaAnulacionAfip],[CodigoRespuestaEnvioSAP],[MensajeRespuestaEnvioSAP],[CodigoRespuestaAnulacionSAP],[MensajeRespuestaAnulacionSAP],[IdEstablecimientoDestinoCambio],[IdClienteDestinatarioCambio],[FechaCreacion],[UsuarioCreacion],[FechaModificacion],[UsuarioModificacion],[IdChoferTransportista],[IdEmpresa]
				,'INSERT' as MOTIVO 
		FROM Solicitudes 
		where IdSolicitud	= @scope

		SELECT @scope
	END


END
