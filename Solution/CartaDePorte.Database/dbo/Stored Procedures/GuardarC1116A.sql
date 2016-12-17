CREATE PROCEDURE [dbo].[GuardarC1116A]
(
	@Idc1116a int,
	@NroCertificadoc1116a	nvarchar(200),
	@CodigoEstablecimiento	int,
	@CuitProveedor	nvarchar(200),
	@RazonSocialProveedor	nvarchar(400),
	@TipoDomicilio	int,
	@CalleRutaProductor	nvarchar(400),
	@NroKmProductor	int,
	@PisoProductor	nvarchar(100),
	@OficinaDtoProductor	nvarchar(100),
	@CodigoLocalidadProductor	int,
	@CodigoPartidoProductor	int,
	@CodigoPostalProductor	nvarchar(100),
	@CodigoEspecie	int,
	@Cosecha	nvarchar(100), 
	@AlmacenajeDiasLibres	int,
	@TarifaAlmacenaje	decimal(18,2),
	@GastoGenerales	decimal(18,2),
	@Zarandeo decimal(18,2),
	@SecadoDe decimal(18,2),
	@SecadoA	decimal(18,2),
	@TarifaSecado decimal(18,2),
	@PuntoExceso  decimal(18,2),
	@TarifaOtros  decimal(18,2),
	@CodigoPartidoOrigen	int,
	@CodigoPartidoEntrega	int,
	@NumeroAnalisis	nvarchar(100),
	@NumeroBoletin	nvarchar(200),
	@FechaAnalisis	datetime,
	@Grado	nvarchar(200),
	@Factor	decimal(18,2),
	@ContenidoProteico	 decimal(18,2),
	@CuitLaboratorio	nvarchar(200),
	@NombreLaboratorio	nvarchar(400),
	@PesoBruto	decimal(18,2),
	@MermaVolatil	decimal(18,2),
	@MermaZarandeo	decimal(18,2),
	@MermaSecado 	decimal(18,2),
	@PesoNeto decimal(18,2),
	@FechaCierre	datetime,
	@ImporteIVAServicios decimal(18,2),
	@TotalServicios decimal(18,2),
	@NumeroCAC	nvarchar(200),
	@Usuario nvarchar(200)

)

AS
BEGIN




	SET NOCOUNT ON;

	IF @Idc1116a > 0
	BEGIN

		UPDATE	c1116a
		SET		NroCertificadoc1116a	=	@NroCertificadoc1116a,
				CodigoEstablecimiento	=	@CodigoEstablecimiento,
				CuitProveedor	=	@CuitProveedor,
				RazonSocialProveedor	=	@RazonSocialProveedor,
				TipoDomicilio	=	@TipoDomicilio,
				CalleRutaProductor	=	@CalleRutaProductor,
				NroKmProductor	=	@NroKmProductor,
				PisoProductor	=	@PisoProductor,
				OficinaDtoProductor	=	@OficinaDtoProductor,
				CodigoLocalidadProductor	=	@CodigoLocalidadProductor,
				CodigoPartidoProductor	=	@CodigoPartidoProductor,
				CodigoPostalProductor	=	@CodigoPostalProductor,
				CodigoEspecie	=	@CodigoEspecie,
				Cosecha	=	@Cosecha,
				AlmacenajeDiasLibres	=	@AlmacenajeDiasLibres,
				TarifaAlmacenaje	=	@TarifaAlmacenaje,
				GastoGenerales	=	@GastoGenerales,
				Zarandeo	=	@Zarandeo,
				SecadoDe	=	@SecadoDe,
				SecadoA	=	@SecadoA,
				TarifaSecado	=	@TarifaSecado,
				PuntoExceso	=	@PuntoExceso,
				TarifaOtros	=	@TarifaOtros,
				CodigoPartidoOrigen	=	@CodigoPartidoOrigen,
				CodigoPartidoEntrega	=	@CodigoPartidoEntrega,
				NumeroAnalisis	=	@NumeroAnalisis,
				NumeroBoletin	=	@NumeroBoletin,
				FechaAnalisis	=	@FechaAnalisis,
				Grado	=	@Grado,
				Factor	=	@Factor,
				ContenidoProteico	=	@ContenidoProteico,
				CuitLaboratorio	=	@CuitLaboratorio,
				NombreLaboratorio	=	@NombreLaboratorio,
				PesoBruto	=	@PesoBruto,
				MermaVolatil	=	@MermaVolatil,
				MermaZarandeo	=	@MermaZarandeo,
				MermaSecado	=	@MermaSecado,
				PesoNeto	=	@PesoNeto,
				FechaCierre	=	@FechaCierre,
				ImporteIVAServicios	=	@ImporteIVAServicios,
				TotalServicios	=	@TotalServicios,
				NumeroCAC	=	@NumeroCAC,
				FechaModificacion = getDate(),
				UsuarioModificacion = @Usuario
		WHERE	Idc1116a	=	@Idc1116a

		SELECT @Idc1116a

	END
	ELSE
	BEGIN


		INSERT INTO c1116a (NroCertificadoc1116a,CodigoEstablecimiento,CuitProveedor,RazonSocialProveedor,TipoDomicilio,
					CalleRutaProductor,NroKmProductor,PisoProductor,OficinaDtoProductor,CodigoLocalidadProductor,CodigoPartidoProductor,
					CodigoPostalProductor,CodigoEspecie,Cosecha,AlmacenajeDiasLibres,TarifaAlmacenaje,GastoGenerales,
					Zarandeo,SecadoDe,SecadoA,TarifaSecado,PuntoExceso,TarifaOtros,CodigoPartidoOrigen,CodigoPartidoEntrega,
					NumeroAnalisis,NumeroBoletin,FechaAnalisis,Grado,Factor,ContenidoProteico,CuitLaboratorio,NombreLaboratorio,
					PesoBruto,MermaVolatil,MermaZarandeo,MermaSecado,PesoNeto,FechaCierre,ImporteIVAServicios,TotalServicios,NumeroCAC, 
					FechaCreacion,	UsuarioCreacion) VALUES 
					(@NroCertificadoc1116a,@CodigoEstablecimiento,@CuitProveedor,@RazonSocialProveedor,@TipoDomicilio,
					@CalleRutaProductor,@NroKmProductor,@PisoProductor,@OficinaDtoProductor,@CodigoLocalidadProductor,@CodigoPartidoProductor,
					@CodigoPostalProductor,@CodigoEspecie,@Cosecha,@AlmacenajeDiasLibres,@TarifaAlmacenaje,@GastoGenerales,
					@Zarandeo,@SecadoDe,@SecadoA,@TarifaSecado,@PuntoExceso,@TarifaOtros,@CodigoPartidoOrigen,@CodigoPartidoEntrega,
					@NumeroAnalisis,@NumeroBoletin,@FechaAnalisis,@Grado,@Factor,@ContenidoProteico,@CuitLaboratorio,@NombreLaboratorio,
					@PesoBruto,@MermaVolatil,@MermaZarandeo,@MermaSecado,@PesoNeto,@FechaCierre,@ImporteIVAServicios,@TotalServicios,@NumeroCAC, 
					getDate(),@Usuario)
		
		DECLARE @scope int
		select @scope = SCOPE_IDENTITY()

		SELECT @scope
	END


END
GO
