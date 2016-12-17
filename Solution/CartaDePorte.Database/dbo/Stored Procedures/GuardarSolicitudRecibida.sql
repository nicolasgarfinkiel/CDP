CREATE PROCEDURE [dbo].[GuardarSolicitudRecibida]
(
	@IdSolicitudRecibida int,
	@IdTipoDeCarta int,
	@NumeroCartaDePorte nvarchar(40),
	@Cee nvarchar(40),
	@Ctg nvarchar(40),
	@FechaDeEmision datetime,
	@idProveedorTitularCartaDePorte nvarchar(40),
	@IdClienteIntermediario nvarchar(40),
	@IdClienteRemitenteComercial nvarchar(40),
	@IdClienteCorredor nvarchar(40),
	@IdClienteEntregador nvarchar(40),
	@IdClienteDestinatario nvarchar(40),
	@IdClienteDestino nvarchar(40),
	@IdProveedorTransportista nvarchar(40),
	@IdChofer nvarchar(40),
	@IdCosecha int,
	@IdEspecie int,
	@NumeroContrato int,
	@CargaPesadaDestino bit,
	@KilogramosEstimados decimal(18,2),
	@IdConformeCondicional int,
	@PesoBruto decimal,
	@PesoTara decimal,
	@Observaciones nvarchar(4000),
	@CodigoEstablecimientoProcedencia nvarchar(40),
	@IdLocalidadEstablecimientoProcedencia int,
	@IdEstablecimientoDestino nvarchar(40),
	@PatenteCamion nvarchar(30),
	@PatenteAcoplado nvarchar(30),
	@KmRecorridos decimal(18,2),
	@EstadoFlete int,
	@CantHoras decimal(18,2),
	@TarifaReferencia decimal(18,2),
	@TarifaReal decimal(18,2),
	@IdGrano int,
	@CodigoEstablecimientoDestinoCambio nvarchar(50),
	@IdLocalidadEstablecimientoDestinoCambio int,
	@CuitEstablecimientoDestinoCambio nvarchar(50),
	@FechaDeDescarga datetime,
	@FechaDeArribo datetime,
	@PesoNetoDescarga decimal,
	@Usuario nvarchar(200),
	@IdEmpresa int

)

AS
BEGIN




	SET NOCOUNT ON;

	IF @IdSolicitudRecibida > 0
	BEGIN

		UPDATE	SolicitudesRecibidas
		SET		IdTipoDeCarta = @IdTipoDeCarta,
				NumeroCartaDePorte = @NumeroCartaDePorte,
				Cee = @Cee,
				Ctg= @Ctg,
				FechaDeEmision = @FechaDeEmision,
				idProveedorTitularCartaDePorte = @idProveedorTitularCartaDePorte,
				IdClienteIntermediario = @IdClienteIntermediario,
				IdClienteRemitenteComercial = @IdClienteRemitenteComercial,
				IdClienteCorredor = @IdClienteCorredor,
				IdClienteEntregador = @IdClienteEntregador,
				IdClienteDestinatario = @IdClienteDestinatario,
				IdClienteDestino = @IdClienteDestino,
				IdProveedorTransportista = @IdProveedorTransportista,
				IdChofer = @IdChofer,
				IdCosecha = @IdCosecha,
				IdEspecie = @IdEspecie,
				NumeroContrato = @NumeroContrato,
				CargaPesadaDestino = @CargaPesadaDestino,
				KilogramosEstimados = @KilogramosEstimados,
				IdConformeCondicional = @IdConformeCondicional,
				PesoBruto = @PesoBruto,
				PesoTara = @PesoTara,
				Observaciones = @Observaciones,
				CodigoEstablecimientoProcedencia = @CodigoEstablecimientoProcedencia,
				IdLocalidadEstablecimientoProcedencia = @IdLocalidadEstablecimientoProcedencia,
				IdEstablecimientoDestino = @IdEstablecimientoDestino,
				PatenteCamion = @PatenteCamion,
				PatenteAcoplado = @PatenteAcoplado,
				KmRecorridos = @KmRecorridos,
				EstadoFlete = @EstadoFlete,
				CantHoras = @CantHoras,
				TarifaReferencia = @TarifaReferencia,
				TarifaReal = @TarifaReal,
				IdGrano = @IdGrano,
				CodigoEstablecimientoDestinoCambio = @CodigoEstablecimientoDestinoCambio,
				IdLocalidadEstablecimientoDestinoCambio = @IdLocalidadEstablecimientoDestinoCambio,
				CuitEstablecimientoDestinoCambio = @CuitEstablecimientoDestinoCambio,
				FechaDeDescarga = @FechaDeDescarga,
				FechaDeArribo = @FechaDeArribo,
				PesoNetoDescarga = @PesoNetoDescarga,
				FechaModificacion = getdate(),
				UsuarioModificacion = @Usuario
		WHERE	IdSolicitudRecibida	=	@IdSolicitudRecibida


	END
	ELSE
	BEGIN

		INSERT INTO SolicitudesRecibidas 
			(IdTipoDeCarta,NumeroCartaDePorte,Cee,Ctg,FechaDeEmision,idProveedorTitularCartaDePorte,IdClienteIntermediario,IdClienteRemitenteComercial,IdClienteCorredor,IdClienteEntregador,IdClienteDestinatario,IdClienteDestino,IdProveedorTransportista,IdChofer,IdCosecha,IdEspecie,NumeroContrato,CargaPesadaDestino,KilogramosEstimados,IdConformeCondicional,PesoBruto,PesoTara,Observaciones,CodigoEstablecimientoProcedencia,IdLocalidadEstablecimientoProcedencia,IdEstablecimientoDestino,PatenteCamion,PatenteAcoplado,KmRecorridos,EstadoFlete,CantHoras,TarifaReferencia,TarifaReal,IdGrano,CodigoEstablecimientoDestinoCambio,IdLocalidadEstablecimientoDestinoCambio,CuitEstablecimientoDestinoCambio,FechaDeDescarga,FechaDeArribo,PesoNetoDescarga,FechaCreacion,UsuarioCreacion,IdEmpresa) 
		VALUES
			(@IdTipoDeCarta,@NumeroCartaDePorte,@Cee,@Ctg,@FechaDeEmision,@idProveedorTitularCartaDePorte,@IdClienteIntermediario,@IdClienteRemitenteComercial,@IdClienteCorredor,@IdClienteEntregador,@IdClienteDestinatario,@IdClienteDestino,@IdProveedorTransportista,@IdChofer,@IdCosecha,@IdEspecie,@NumeroContrato,@CargaPesadaDestino,@KilogramosEstimados,@IdConformeCondicional,@PesoBruto,@PesoTara,@Observaciones,@CodigoEstablecimientoProcedencia,@IdLocalidadEstablecimientoProcedencia,@IdEstablecimientoDestino,@PatenteCamion,@PatenteAcoplado,@KmRecorridos,@EstadoFlete,@CantHoras,@TarifaReferencia,@TarifaReal,@IdGrano,@CodigoEstablecimientoDestinoCambio,@IdLocalidadEstablecimientoDestinoCambio,@CuitEstablecimientoDestinoCambio,@FechaDeDescarga,@FechaDeArribo,@PesoNetoDescarga, getDate(),@Usuario,@IdEmpresa)
		
		DECLARE @scope int
		select @scope = SCOPE_IDENTITY()


		SELECT @scope
	END


END

